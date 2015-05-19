using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Runtime.Serialization;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
using System.Timers;

namespace LearningCard.Model
{
    [ServiceContract(CallbackContract = typeof(IBroadcastorCallBack))]
    internal interface IOnlineLobbyService
    {
        [OperationContract(IsOneWay=false)]
        void JoinToLobby(String name);
        [OperationContract(IsOneWay = true)]
        void NotifyServer(EventDataType eventData);
        [OperationContract(IsOneWay=false)]
        List<String> GetActiveUsers();
        [OperationContract]
        String GetPublicIP();
    }

    [DataContract()]
    public class EventDataType
    {
        [DataMember]
        public String ClientName { get; set; }
        [DataMember]
        public String EventMessage { get; set; }
    }

    public interface IBroadcastorCallBack
    {
        [OperationContract(IsOneWay = true)]
        void BroadcastToClient(EventDataType eventData);
    }

    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    class OnlineLobbyServiceModel : IOnlineLobbyService
    {
        private static Dictionary<String, IBroadcastorCallBack> clients = new Dictionary<string, IBroadcastorCallBack>();

        private String _HostIP;
        private Uri baseAddress;
        private List<Profile> ActiveUsers;

        public String HostIP
        {
            get
            {
                return this._HostIP;
            }
            set
            {
                if (this.isHost)
                {
                    this._HostIP = value;
                }
            }
        }
        public Boolean isHost { get; set; }
        public List<Profile> _JoinedPlayers { get; set; }

        public OnlineLobbyServiceModel()
        {   
            this.ActiveUsers = new List<Profile>();

        }

        public void JoinToLobby(String name)
        {
            if (name != null && name != "")
            {
                try
                {
                    IBroadcastorCallBack callback = OperationContext.Current.GetCallbackChannel<IBroadcastorCallBack>();
                    if (clients.ContainsKey(name))
                    {
                        clients.Remove(name);
                    }
                    clients.Add(name, callback);
                }
                catch (Exception)
                { }
            }
        }

        public List<String> GetActiveUsers()
        {
            return new List<string>(clients.Keys);
        }

        public void NotifyServer(EventDataType eventData)
        {
            List<String> inactiveClients = new List<string>();
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
            foreach (var client in inactiveClients)
            {
                clients.Remove(client);
            }
        }

        public String GetPublicIP()
        {
            String ip = this.GetPublicIPFromWhatIsMyIP();
            if (ip == null)
            {
                ip = this.GetPublicIPFromDynDNS();
            }
            if (ip == null)
            {
                System.Windows.Forms.MessageBox.Show("Could not reach internet\nYou will be able to play on local network.", "There is no internet",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return "127.0.0.1";
            }
            return ip;
        }
        private String GetPublicIPFromDynDNS()
        {
            WebRequest webRequest = WebRequest.Create("http://checkip.dyndns.org");
            webRequest.Timeout = 5000;
            WebResponse webResp;
            try
            {
                webResp = webRequest.GetResponse();
            }
            catch (WebException e)
            {
                return null;
            }
            StreamReader resp = new StreamReader(webResp.GetResponseStream());
            String fullResp = resp.ReadToEnd().Trim();
            return fullResp.Split(':')[1].Substring(1).Split('<')[0]; ;
        }
        private String GetPublicIPFromWhatIsMyIP()
        {
            WebRequest webRequest = WebRequest.Create("http://whatismyipaddress.com/");
            webRequest.Timeout = 5000;
            WebResponse webResp;
            try
            {
                webResp = webRequest.GetResponse();
            }
            catch (WebException e)
            {
                return null;
            }
            StreamReader resp = new StreamReader(webResp.GetResponseStream());
            String fullResp = resp.ReadToEnd().Trim();
            Regex ipPattern = new Regex(@">((?:\d{1,3}\.){3}\d{1,3})<\/a>");
            Match m = ipPattern.Match(fullResp);
            return m.Groups[1].ToString();
        }

    }
}
