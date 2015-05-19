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
        private static object locker = new object();
        private CardPack deck;

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

        public void SetupDeck(CardPack cp)
        {
            this.deck = cp;
        }
    }
}
