using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCardClasses
{
    [DataContract]
    public class Card
    {
    	[DataMember]
        public String Title { get; set; }
        [DataMember]
        public IQuestion Question { get; set; }
        [DataMember]
        public IAnswer Answer { get; set; }
    }
}
