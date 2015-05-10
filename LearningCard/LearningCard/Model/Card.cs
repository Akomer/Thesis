using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract]
    class Card
    {
    	[DataMember]
        public String Title { get; set; }
        [DataMember]
        public IQuestion Question { get; set; }
        [DataMember]
        public IAnswer Answer { get; set; }
    }
}
