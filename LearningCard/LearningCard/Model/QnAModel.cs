using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace LearningCard.Model
{
    class QnAModel
    {
        private List<Model.Card> CardPack;

        public QnAModel(String fileName)
        {
            using (FileStream loadFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Model.Card>));
                this.CardPack = (List<Model.Card>)serializer.ReadObject(loadFile);
            }
        }

        public Card GetCard()
        {
            return this.CardPack[0];
        }
    }
}
