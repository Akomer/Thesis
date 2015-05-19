using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace LearningCard.Model
{
    class OnlineLobbyClientModel : OnlineLearningCardService.IOnlineLobbyServiceCallback
    {
        private System.Threading.SynchronizationContext syncContext =
            System.ComponentModel.AsyncOperationManager.SynchronizationContext;

        private EventHandler _broadcastorCallBackHandler;
        public void SetHandler(EventHandler handler)
        {
            this._broadcastorCallBackHandler = handler;
        }

        public void BroadcastToClient(OnlineLearningCardService.EventDataType eventData)
        {
            syncContext.Post(new System.Threading.SendOrPostCallback(this.OnBroadcast), eventData);
        }
        private void OnBroadcast(object eventData)
        {
            this._broadcastorCallBackHandler.Invoke(eventData, null);
        }

        private delegate void HandleBroadcastCallback(object sender, EventArgs e);
        public void HandleBroadcast(object sender, EventArgs e)
        {
            try
            {
                var eventData = (OnlineLearningCardService.EventDataType)sender;
                if (eventData.EventMessage == "NewMember")
                {
                    this.OnNewPlayerJoined();
                }
                    //eventData.EventMessage, eventData.ClientName);
            }
            catch (Exception ex)
            {
            }
        }

        private void JoinToLobby()
        {
            if (this._client != null)
            {
                this._client.Abort();
                this._client = null;
            }

            //OnlineLobbyClientModel lobbyClient = new OnlineLobbyClientModel(false);
            this.SetHandler(this.HandleBroadcast);

            System.ServiceModel.InstanceContext context = new InstanceContext(this);
            this._client = new OnlineLearningCardService.OnlineLobbyServiceClient(context);
            this._client.JoinToLobby("Client A Name");
            this._client.NotifyServer(new OnlineLearningCardService.EventDataType() { ClientName = "Client G Name", EventMessage = "NewMember" });
        }

        private OnlineLearningCardService.OnlineLobbyServiceClient _client;

        private Int32 serverStatus;

        public String HostIP 
        {
            get
            {
//                return LobbyClient.GetPublicIP();
                return "as";
            }
            set
            { }
        }
        public Boolean isHost { get; set; }

        public OnlineLobbyClientModel(Boolean ishost, String hostip = "127.0.0.1")
        {
            this.isHost = ishost;
            if (isHost)
            {
                System.Net.ServicePointManager.DefaultConnectionLimit = 10;
                Thread service = new Thread(this.StartLobbyService);
                service.Start();
            }
            try
            {
                this.JoinToLobby();
            }
            catch (EndpointNotFoundException e)
            {
                {
                    System.Windows.Forms.MessageBox.Show("Could not connect to the server.", "Connection Error",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    throw new InvalidOperationException();
                }
            }
            
        }

        public List<String> GetActiveUsers()
        {
            var a = this._client.GetActiveUsers();
            // var b = this._client.GetRandomString();
            //var a = new Lost<String>{"a", "b"};
            return a;
        }



        private void StartLobbyService()
        {
            Uri baseAddress = new Uri("net.tcp://localhost:8080/learningcard/");
            ServiceHost lobbyHost = new ServiceHost(typeof(Model.OnlineLobbyServiceModel), baseAddress);
            {
                lobbyHost.OpenTimeout = new System.TimeSpan(1, 0, 0);
                try
                {
                    lobbyHost.Open();
                }
                catch (AddressAccessDeniedException e)
                {
                    System.Windows.Forms.MessageBox.Show("Can not start server, if you are not adminstrator\nTry to start the program in administrator mode.", "Admin mode",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    this.serverStatus = 1;
                    return;
                }
                catch (Exception)
                {
                    System.Windows.Forms.MessageBox.Show("Unkown issue has occured.", "N/A error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    this.serverStatus = 1;
                    return;
                }
            }
        }

        public event EventHandler NewPlayerJoined;

        private void OnNewPlayerJoined()
        {
            if (this.NewPlayerJoined != null)
            {
                this.NewPlayerJoined(this, new EventArgs());
            }
        }
    }
}
