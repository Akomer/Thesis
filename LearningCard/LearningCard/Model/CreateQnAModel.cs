using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;

namespace LearningCard.Model
{
    class CreateQnAModel
    {
        public List<Model.Card> CardPack;

        public CreateQnAModel()
        {
            this.CardPack = new List<Card>();
        }

        public void AddCard(String title, IQuestion question, IAnswer answer)
        {
            this.CardPack.Add(new Card() { Title = title, Question = question, Answer = answer });
        }

        public void SaveCardPack(String fileName)
        {
            using (FileStream saveFile = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Model.Card>));
                serializer.WriteObject(saveFile, this.CardPack);
            }
        }

        public void LoadCardPack(String fileName)
        {
            using (FileStream loadFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Model.Card>));
                this.CardPack = (List<Model.Card>)serializer.ReadObject(loadFile);
            }
        }
    }
}
