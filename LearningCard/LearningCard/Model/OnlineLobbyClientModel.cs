using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;

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
            this.RegisterClient();
            this.Host = isHost;
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

        public void SetupDeckOnServer()
        {
            if (this._Deck != null)
            {
                this._client.SetupDeck(this._Deck);
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

        

    }
}
