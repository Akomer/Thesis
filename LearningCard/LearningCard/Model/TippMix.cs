using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract]
    class TippMix
    {
        [DataMember]
        public Boolean IsChecked { get; set; }
        [DataMember]
        public String TippText { get; set; }
    }
}
