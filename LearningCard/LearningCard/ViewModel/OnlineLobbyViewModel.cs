using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace LearningCard.ViewModel
{
    class OnlineLobbyViewModel : MainWindowViewModel
    {
        private Model.OnlineLobbyClientModel LobbyClient;

        public ObservableCollection<OnlineLearningCardService.Profile> ActiveUserProfileList 
        {
            get
            {
                return new ObservableCollection<OnlineLearningCardService.Profile>(this.LobbyClient.GetActiveUsers());
            }
            set
            { }
        }
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

        public OnlineLobbyViewModel()
            : this(true, "127.0.0.1") {}

        public OnlineLobbyViewModel(Boolean isHost)
            : this(isHost, "127.0.0.1")
        { }

        public OnlineLobbyViewModel(Boolean isHost, String hostip = "127.0.0.1")
        {
            try
            {
                this.LobbyClient = new Model.OnlineLobbyClientModel(isHost, hostip);
            }
            catch (InvalidOperationException e)
            {
                this.LobbyClient = null;
            }

            if (this.LobbyClient == null)
            {
                //throw new NotImplementedException();
                return;
            }
            else
            {
                this.Command_RefresIP = new DelegateCommand(x => this.Execute_RefresIP());

                this.RefreshView();
            }
        }

        public override bool IsReady()
        {
            return this.LobbyClient != null;
        }

        private void Execute_RefresIP()
        {
            //this.OnPropertyChanged("HostIpAddres");
        }

        private void RefreshView()
        {
            this.OnPropertyChanged("HostIpAddres");
            //this.OnPropertyChanged("ActiveUserProfileList");
        }
    }
}
