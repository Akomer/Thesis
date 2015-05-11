using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract]
    class Profile
    {
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        private Dictionary<String, List<Int32> > StatisticData { get; set; }

        public List<Int32> GetStatInfo(String DeckName)
        {
            List<Int32> o;
            if (StatisticData.TryGetValue(DeckName, out o))
                return o;
            return null;
        }
    }
}
