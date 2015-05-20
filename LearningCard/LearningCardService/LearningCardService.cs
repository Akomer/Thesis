using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using LearningCardClasses;

namespace LearningCardService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class LearningCardService : ILearningCardService
    {
        private static Dictionary<string, IServiceCallBack> clients = new Dictionary<string, IServiceCallBack>();
        private static Dictionary<String, Int32> clientAnserws;
        private static Dictionary<String, Int32> clientScores;
        private static object locker = new object();
        private static CardPack deck;
        private static Int32 cardIndex;
        private static ServerStatus status = ServerStatus.Lobby;

        enum ServerStatus { Lobby, InGame };

        public void RegisterClient(string clientName)
        {
            if (clientName != null && clientName != "")
            {
                try
                {
                    IServiceCallBack callback = OperationContext.Current.GetCallbackChannel<IServiceCallBack>();
                    lock (locker)
                    {
                        //remove the old client
                        if (clients.Keys.Contains(clientName))
                            clients.Remove(clientName);
                        clients.Add(clientName, callback);
                        this.NotifyServer(new EventDataType() { ClientName = clientName, EventMessage = "NewMemberArrived" });
                    }
                }
                catch (Exception)
                {
                }
            }
        }

        public void NotifyServer(EventDataType eventData)
        {
            lock (locker)
            {
                var inactiveClients = new List<string>();
                foreach (var client in clients)
                {
                    if (client.Key != eventData.ClientName)
                    {
                        try
                        {
                            client.Value.BroadcastToClient(eventData);
                        }
                        catch (Exception)
                        {
                            inactiveClients.Add(client.Key);
                        }
                    }
                }

                if (inactiveClients.Count > 0)
                {
                    foreach (var client in inactiveClients)
                    {
                        clients.Remove(client);
                    }
                    this.NotifyServer(new EventDataType() { ClientName = "server", EventMessage = "MemberDisconnected" });
                }
            }
        }

        public List<String> GetActivePlayers()
        {
            lock (locker)
            {
                return new List<String>(clients.Keys);
            }
        }

        public void StartGame(CardPack cp)
        {
            deck = cp;
            status = ServerStatus.InGame;
            clientScores = new Dictionary<string, int>();
            foreach (var client in clients.Keys)
            {
                clientScores.Add(client, 0);
            }
            NextTurn();
        }

        private void NextTurn()
        {
            clientAnserws = new Dictionary<string, Int32>();
            foreach(var client in clients.Keys)
            {
                clientAnserws.Add(client, -1);
            }
            Random r = new Random();
            cardIndex = r.Next(deck.CardList.Count);
        }

        public Card GetCard()
        {
            return deck.CardList[cardIndex];
        }

        public void SendAnswer(String clientName, Boolean IsRight)
        {
            if (IsRight)
            {
                clientAnserws[clientName] = 1;
            }
            else
            {
                clientAnserws[clientName] = 0;
            }
            clientScores[clientName] += clientAnserws[clientName];
            if(EveryoneSentAnswer())
            {
                NextTurn();
            }
        }

        private Boolean EveryoneSentAnswer()
        {
            foreach (var item in clientAnserws.Values)
            {
                if (item == -1)
                    return false;
            }
            return true;
        }

        public void SetVisibleDeckName(String name)
        {
            deck = new CardPack() { PackName = name };
            this.NotifyServer(new EventDataType() { ClientName="Server", EventMessage="CardPackChanged"});
        }

        public String GetVisibleDeckName()
        {
            if (deck == null)
            {
                return "";
            }
            return deck.PackName;
        }

        public CardPack GetServerDeck()
        {
            return deck;
        }

        public Dictionary<String, int> GetScoreBoard()
        {
            return clientScores;
        }
    }
}
