using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class OnlineLobbyViewModel : MainViewModelBase
    {
        private Model.OnlineLobbyClientModel lobbyClient;

        public ObservableCollection<Profile> ActiveUserProfileList 
        {
            get
            {
                ObservableCollection<Profile> obc = new ObservableCollection<Profile>();
                foreach (var item in lobbyClient.GetActivePlayers())
                {
                    obc.Add(new Profile(item));
                }
                return obc;
            }
            set
            { }
        }
        public String ActivePackName
        {
            get
            {
                if (this.lobbyClient.Deck == null)
                {
                    return Model.GlobalLanguage.Instance.GetDict()["Nothing"];
                }
                return this.lobbyClient.Deck.PackName;
            }
        }
        public DelegateCommand Command_RefresIP { get; set; }
        public DelegateCommand Command_LoadCardPack { get; set; }
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
            this.Command_RefresIP = new DelegateCommand(x => this.Execute_RefreshIP());
            this.Command_LoadCardPack = new DelegateCommand(x => this.Execute_LoadCardPack());
            this.RefreshView();
        }

        public override bool IsReady()
        {
            //return this.LobbyClient != null;
            return true;
        }

        private void Execute_RefreshIP()
        {
            this.RefreshView();
        }

        private void RefreshViewEvent(object sender, EventArgs e)
        {
            this.RefreshView();
        }

        private void RefreshView()
        {
            this.OnPropertyChanged("HostIpAddres");
            this.OnPropertyChanged("ActivePackName");
            this.OnPropertyChanged("ActiveUserProfileList");
        }

        private void Execute_LoadCardPack()
        {
            View.LoadCardPackDialog newDialog = new View.LoadCardPackDialog();
            LoadCardPackDiagViewModel diagVM = new LoadCardPackDiagViewModel(newDialog);
            newDialog.DataContext = diagVM;

            if (newDialog.ShowDialog() == true)
            {
                this.lobbyClient.Deck = diagVM.GetSelectedItem();
                this.OnPropertyChanged("ActivePackName");
            }
        }
    }
}
