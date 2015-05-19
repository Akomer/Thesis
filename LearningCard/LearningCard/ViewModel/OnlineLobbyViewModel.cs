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
        private Model.OnlineLobbyClientModel LobbyClient;

        public ObservableCollection<Model.Profile> ActiveUserProfileList 
        {
            get
            {
                ObservableCollection<Model.Profile> obc = new ObservableCollection<Model.Profile>();
                foreach (var item in this.LobbyClient.GetActiveUsers())
                    obc.Add(new Model.Profile(item));
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
                this.LobbyClient.NewPlayerJoined += new EventHandler(this.RefreshViewEvent);
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

        private void RefreshViewEvent(object sender, EventArgs e)
        {
            this.RefreshView();
        }

        private void RefreshView()
        {
            this.OnPropertyChanged("HostIpAddres");
            var a = this.LobbyClient.GetActiveUsers();
            this.OnPropertyChanged("ActiveUserProfileList");
        }
    }
}
