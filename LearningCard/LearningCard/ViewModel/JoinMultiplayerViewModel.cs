using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.ServiceModel;

namespace LearningCard.ViewModel
{
    class JoinMultiplayerViewModel : MainViewModelBase
    {
        public DelegateCommand Command_JoinToServer { get; set; }
        public String HostIPAddress { get; set; }

        public JoinMultiplayerViewModel()
        {
            this.HostIPAddress = "000.000.000.000";
            this.Command_JoinToServer = new DelegateCommand(x => this.Execute_JoinToServer());
        }

        private void Execute_JoinToServer()
        {
            Regex ip = new Regex(@"(\d{1,3}\.){3}\d{1,3}");
            Match m = ip.Match(this.HostIPAddress);
            if ( m.Value == "" )
            {
                System.Windows.Forms.MessageBox.Show("Invalid IP address format", "Invalid IP",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            try
            {
                CheckEndpoint();
            }
            catch (EndpointNotFoundException)
            {
                System.Windows.Forms.MessageBox.Show(Model.GlobalLanguage.Instance.GetDict()["CouldNotConnectToTheServer"], 
                    Model.GlobalLanguage.Instance.GetDict()["InalidIP"],
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            this.OnChangeMainWindowContent(typeof(View.OnlineLobbyUserControl), typeof(ViewModel.OnlineLobbyViewModel),
                new object[] { false, this.HostIPAddress });
        }

        private void CheckEndpoint()
        {
            System.Net.Sockets.Socket sock = new System.Net.Sockets.Socket(System.Net.Sockets.AddressFamily.InterNetwork,
                                                                           System.Net.Sockets.SocketType.Stream, 
                                                                           System.Net.Sockets.ProtocolType.Tcp);
            sock.ReceiveTimeout = sock.SendTimeout = 1000;
            IAsyncResult res = sock.BeginConnect(this.HostIPAddress, 8080, null, null);
            if (!res.AsyncWaitHandle.WaitOne(1000, true))
            {
                throw new EndpointNotFoundException();
            }
        }
    }
}
