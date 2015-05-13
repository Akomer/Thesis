using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class OnlineLobbyServiceClient : System.ServiceModel.ClientBase<IOnlineLobbyServiceModel>, IOnlineLobbyServiceModel
    {
        public OnlineLobbyServiceClient() { }
        public OnlineLobbyServiceClient(string configurationName) :
            base(configurationName)
        { }

        public OnlineLobbyServiceClient(System.ServiceModel.Description.ServiceEndpoint address) :
            base(address)
        { }


    }
}
