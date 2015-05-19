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
                // OnlineLearningCardService.OnlineLobbyServiceClient connection_test;
                // String endp = "net.tcp://" + this.HostIPAddress + "/learningcard/";
                // connection_test = new OnlineLearningCardService.OnlineLobbyServiceClient(new InstanceContext(new Model.OnlineLobbyClientModel(false)), new System.ServiceModel.NetTcpBinding(),
                //    new EndpointAddress(endp));
                // connection_test.GetPublicIP();
            }
            catch (EndpointNotFoundException e)
            {
                System.Windows.Forms.MessageBox.Show("Could connect to the server", "Invalid IP",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            this.OnChangeMainWindowContent(typeof(View.OnlineLobbyUserControl), typeof(ViewModel.OnlineLobbyViewModel),
                new object[] { false, this.HostIPAddress });
        }
    }
}
