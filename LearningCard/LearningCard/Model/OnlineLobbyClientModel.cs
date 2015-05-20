using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;
using System.ServiceModel;

namespace LearningCard.Model
{
    class OnlineLobbyClientModel : ServiceCallBack
    {
        private CardPack _Deck;
        public CardPack Deck 
        { 
            get
            {
                return this._client.GetServerDeck();
            }
            set
            {
                this.setDeck(value);
            }
        }
        public Boolean Host { get; set; }

        public OnlineLobbyClientModel()
        {
            this.RegisterClient();
        }

        public OnlineLobbyClientModel(Boolean isHost) 
        {
            this.Host = isHost;
            if (isHost)
            {
                ServiceHost host = new ServiceHost(typeof(LearningCardService.LearningCardService),
                    new Uri[] { new Uri("net.tcp://localhost:8080/LearningCardService/Service1/") });
                foreach (var item in host.Description.Behaviors)
                {
                    if (item.GetType() == typeof(System.ServiceModel.Description.ServiceMetadataBehavior))
                    {
                        ((System.ServiceModel.Description.ServiceMetadataBehavior)item).HttpGetEnabled = false;
                        ((System.ServiceModel.Description.ServiceMetadataBehavior)item).HttpsGetEnabled = false;
                    }
                }
                try
                {
                    System.Threading.Thread tr = new System.Threading.Thread(x => this.StartHost(host));
                    tr.Start();
                }
                catch (AddressAlreadyInUseException)
                {
                    System.Windows.Forms.MessageBox.Show(Model.GlobalLanguage.Instance.GetDict()["ServerIsAlreadyRunning"],
                        Model.GlobalLanguage.Instance.GetDict()["None"],
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                }
            }
            this.RegisterClient();
        }

        private void StartHost(ServiceHost host)
        {
            try
            {
                host.Open();
            }
            catch (AddressAlreadyInUseException)
            {
                System.Windows.Forms.MessageBox.Show(Model.GlobalLanguage.Instance.GetDict()["ServerIsAlreadyRunning"],
                    Model.GlobalLanguage.Instance.GetDict()["None"],
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
            }
        }

        public List<String> GetActivePlayers()
        {
            var a = this._client.GetActivePlayersAsync().Result;
            return a;
        }

        private async void setDeck(CardPack cp)
        {
            this._Deck = cp;
            await Task.Delay(50);
            this._client.SetVisibleDeckName(this._Deck.PackName);
        }

        public override async void HandleBroadcast(object sender, EventArgs e)
        {
            try
            {
                EventDataType eventData = (EventDataType)sender;
                await Task.Delay(50);
                if (eventData.EventMessage == "NewMemberArrived" || eventData.EventMessage == "MemberDisconnected")
                {
                    this.OnNewPlayerJoined();
                    return;
                }
                if (eventData.EventMessage == "CardPackChanged")
                {
                    this.OnSelectedCardPackChanged();
                    return;
                }
            }
            catch (Exception)
            {
            }
        }

        public event EventHandler SelectedCardPackChanged;
        public event EventHandler NewPlayerJoined;

        private void OnSelectedCardPackChanged()
        {
            if (this.SelectedCardPackChanged != null)
            {
                this.SelectedCardPackChanged(this, new EventArgs());
            }
        }
        private void OnNewPlayerJoined()
        {
            if (this.NewPlayerJoined != null)
            {
                this.NewPlayerJoined(this, new EventArgs());
            }
        }

        public OnlineLearningCardService.LearningCardServiceClient GetServiceClient()
        {
            return this._client;
        }

        public void StartGame()
        {
            this._client.StartGame(this._Deck);
        }

    }
}
