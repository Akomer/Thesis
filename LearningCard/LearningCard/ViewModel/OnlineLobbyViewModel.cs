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
    class OnlineLobbyViewModel : MainViewModelBase
    {
        private Model.OnlineLobbyClientModel lobbyClient;

        public ObservableCollection<Model.Profile> ActiveUserProfileList 
        {
            get
            {
                ObservableCollection<Model.Profile> obc = new ObservableCollection<Model.Profile>();
                foreach (var item in lobbyClient.GetActivePlayers())
                {
                    obc.Add(new Model.Profile(item));
                }
                return obc;
            }
            set
            { }
        }
        public DelegateCommand Command_RefresIP { get; set; }
        public String HostIpAddres 
        {
            get
            {
                return "IP.Adr";
            }
            set { }
        }

        public OnlineLobbyViewModel()
            : this(true, "127.0.0.1") {}

        public OnlineLobbyViewModel(Boolean isHost)
            : this(isHost, "127.0.0.1")
        { }

        public OnlineLobbyViewModel(Boolean isHost, String hostip = "127.0.0.1")
        {
            this.lobbyClient = new Model.OnlineLobbyClientModel();
            this.lobbyClient.NewPlayerJoined += new EventHandler(this.RefreshViewEvent);
            //this.Command_RefresIP = new DelegateCommand(x => this.Execute_RefresIP());
            this.RefreshView();
        }

        public override bool IsReady()
        {
            //return this.LobbyClient != null;
            return true;
        }

        private void Execute_RefresIP()
        {

        }

        private void RefreshViewEvent(object sender, EventArgs e)
        {
            this.RefreshView();
        }

        private void RefreshView()
        {
            this.OnPropertyChanged("HostIpAddres");
            this.OnPropertyChanged("ActiveUserProfileList");
        }
    }
}
