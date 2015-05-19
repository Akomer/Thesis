using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.Runtime.Serialization;

namespace LearningCardClasses
{
    [DataContract()]
    public class EventDataType
    {
        [DataMember]
        public string ClientName { get; set; }
        [DataMember]
        public string EventMessage { get; set; }
    }
}
