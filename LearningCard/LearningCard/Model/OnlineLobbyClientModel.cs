using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.ServiceModel;

namespace LearningCard.Model
{
    class OnlineLobbyClientModel
    {
        private OnlineLobbyServiceModel LobbyService;

        public String HostIP { get; set; }
        public Boolean isHost { get; set; }

        public OnlineLobbyClientModel(Boolean ishost)
        {
            this.isHost = ishost;
            if (isHost)
            {
                Thread service = new Thread(this.StartLobbyService);
                service.Start();
            }

            
        }


        public void GetPublicIP()
        {
            
        }

        private void StartLobbyService()
        {
            this.LobbyService = new OnlineLobbyServiceModel();
        }
    }
}
