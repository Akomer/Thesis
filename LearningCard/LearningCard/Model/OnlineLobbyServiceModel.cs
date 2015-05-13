﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Runtime.Serialization.Json;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;

namespace LearningCard.Model
{
    [ServiceContract]
    internal interface IOnlineLobbyServiceModel
    {
        [OperationContract]
        Boolean JoinToLobby();

        [OperationContract]
        String GetPublicIP();
    }

    class OnlineLobbyServiceModel
    {
        private String _HostIP;
        private Uri baseAddress;

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
            this.isHost = true;
            if (!isHost)
            {
                return;
            }
            this.HostIP = this.GetPublicIP();
            this.baseAddress = new Uri("http://localhost:8080/learningcard/");
            ServiceHost lobbyHost = new ServiceHost(typeof(OnlineLobbyServiceModel), this.baseAddress);
            {
                ServiceMetadataBehavior smdb = new ServiceMetadataBehavior();
                smdb.HttpGetEnabled = true;
                smdb.MetadataExporter.PolicyVersion = PolicyVersion.Policy15;
                lobbyHost.Description.Behaviors.Add(smdb);

                lobbyHost.Open();
            }
        }

        public Boolean JoinToLobby()
        {
            return true;
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

        [OperationContract]
        public List<Profile> GetJoinedPlayers()
        {
            return this._JoinedPlayers;
        }
    }
}
