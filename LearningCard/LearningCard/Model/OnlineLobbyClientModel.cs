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
    class OnlineLobbyClientModel
    {
        private OnlineLobbyServiceModel LobbyService;
        private OnlineLearningCardService.OnlineLobbyServiceModelClient LobbyClient;

        public String HostIP 
        {
            get
            {
                return LobbyClient.GetPublicIP();
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
                Thread service = new Thread(this.StartLobbyService);
                service.Start();
            }
            //this.LobbyClient = new OnlineLearningCardService.OnlineLobbyServiceModelClient(@"http://" + "86.59.238.248" + @":8080/learningcard/");
            this.LobbyClient = new OnlineLearningCardService.OnlineLobbyServiceModelClient(new System.ServiceModel.BasicHttpBinding(),
                new System.ServiceModel.EndpointAddress(@"http://" + hostip + @":8080/learningcard/"));
            //this.LobbyClient = new OnlineLearningCardService.OnlineLobbyServiceModelClient();
            OnlineLearningCardService.Profile p = new OnlineLearningCardService.Profile() { Name = "Guest", StatisticData = new Dictionary<string, int[]>() };
            this.LobbyClient.JoinToLobby(p);
            
        }

        public OnlineLearningCardService.Profile[] GetActiveUsers()
        {
            return this.LobbyClient.GetActiveUsers();
        }

        private void StartLobbyService()
        {
            Uri baseAddress = new Uri("http://localhost:8080/learningcard/");
            //Uri baseAddress = new Uri("http://86.59.238.248:8080/learningcard/");
            ServiceHost lobbyHost = new ServiceHost(typeof(OnlineLobbyServiceModel), baseAddress);
            {
                ServiceMetadataBehavior smdb = new ServiceMetadataBehavior();
                smdb.HttpGetEnabled = true;
                smdb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                lobbyHost.Description.Behaviors.Add(smdb);
                lobbyHost.OpenTimeout = new System.TimeSpan(1, 0, 0);
                lobbyHost.AddDefaultEndpoints();

                lobbyHost.Open();
            }
        }
    }
}
