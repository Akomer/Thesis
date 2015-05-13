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

        public OnlineLobbyClientModel(Boolean ishost)
        {
            this.isHost = ishost;
            if (isHost)
            {
                Thread service = new Thread(this.StartLobbyService);
                service.Start();
            }
            this.LobbyClient = new OnlineLearningCardService.OnlineLobbyServiceModelClient();
            
        }

        private void StartLobbyService()
        {
            Uri baseAddress = new Uri("http://localhost:8080/learningcard/");
            ServiceHost lobbyHost = new ServiceHost(typeof(OnlineLobbyServiceModel), baseAddress);
            {
                ServiceMetadataBehavior smdb = new ServiceMetadataBehavior();
                smdb.HttpGetEnabled = true;
                smdb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                lobbyHost.Description.Behaviors.Add(smdb);

                lobbyHost.Open();
            }
        }
    }
}
