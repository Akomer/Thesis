using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract]
    class CardPack
    {
        [DataMember]
        public String PackName { get; set; }
        [DataMember]
        public List<Card> CardList { get; set; }
    }
}
