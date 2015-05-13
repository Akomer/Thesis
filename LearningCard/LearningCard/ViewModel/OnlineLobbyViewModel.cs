using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

namespace LearningCard.ViewModel
{
    class OnlineLobbyViewModel : MainWindowViewModel
    {
        private Model.OnlineLobbyClientModel LobbyClient;

        public DelegateCommand Command_RefresIP { get; set; }
        public String HostIpAddres 
        {
            get
            {
                return this.LobbyClient.HostIP;
            }
            set { }
        }
        public Boolean Host
        {
            get
            {
                return this.LobbyClient.isHost;
            }
            set
            {
                this.LobbyClient.isHost = value;
            }
        }

        public OnlineLobbyViewModel(Boolean isHost)
        {
            this.LobbyClient = new Model.OnlineLobbyClientModel(isHost);
            this.Command_RefresIP = new DelegateCommand(x => this.Execute_RefresIP());

            this.RefreshView();
        }

        private void Execute_RefresIP()
        {
            this.LobbyClient.GetPublicIP();
        }

        private void RefreshView()
        {
            this.OnPropertyChanged("HostIpAddres");
        }
    }
}
