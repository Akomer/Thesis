using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace LearningCardService
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IService1" in both code and config file together.
    [ServiceContract(CallbackContract = typeof(IServiceCallBack))]
    public interface ILearningCardService
    {
        [OperationContract(IsOneWay = true)]
        void RegisterClient(string clientName);

        [OperationContract(IsOneWay = true)]
        void NotifyServer(EventDataType eventData);

        [OperationContract]
        List<String> GetActivePlayers();
    }

    [DataContract()]
    public class EventDataType
    {
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string EventMessage { get; set; }
    }

    public interface IServiceCallBack
    {
        [OperationContract(IsOneWay = true)]
        void BroadcastToClient(EventDataType eventData);
    }
} 
