using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class OnlineLobbyClientModel : ServiceCallBack
    {
        public OnlineLobbyClientModel()
        {
            this.RegisterClient();
        }

        public List<String> GetActivePlayers()
        {
            var a = this._client.GetActivePlayersAsync().Result;
            return a;
        }

        public override async void HandleBroadcast(object sender, EventArgs e)
        {
            try
            {
                LearningCardService.EventDataType eventData = (LearningCardService.EventDataType)sender;
                if (eventData.EventMessage == "NewMemberArrived" || eventData.EventMessage == "MemberDisconnected")
                {
                    await Task.Delay(50);
                    this.OnNewPlayerJoined();
                }
            }
            catch (Exception ex)
            {
            }
        }

        public event EventHandler NewPlayerJoined;
        private void OnNewPlayerJoined()
        {
            if (this.NewPlayerJoined != null)
            {
                this.NewPlayerJoined(this, new EventArgs());
            }
        }

    }
}
