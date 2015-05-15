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

        public Profile(String name = "Guest")
        {
            this.Name = name;
            this.StatisticData = new Dictionary<string, List<int>>();
        }

        public List<Int32> GetStatInfo(String DeckName)
        {
            List<Int32> o;
            if (StatisticData.TryGetValue(DeckName, out o))
                return o;
            return null;
        }

        public void AddNewStatData(CardPack cards)
        {
            List<Int32> l = new List<int>();
            foreach (var i in cards.CardList)
            {
                l.Add(5);
            }
            this.StatisticData.Add(cards.PackName, l);
        }

        public OnlineLearningCardService.Profile GetServiceProfile()
        {
            return new OnlineLearningCardService.Profile() { Name = this.Name };
        }
    }
}
