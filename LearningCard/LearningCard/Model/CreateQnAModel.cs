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
        public Uri SourceFile;
        public List<Model.Card> CardPack;
        // public List<String> CardPack;

        public CreateQnAModel()
        {
            this.CardPack = new List<Card>();
        }

        // public void AddCard(String title)
        // {
        //     this.CardPack.Add(new Card() { Title = title });
        // }

        public void AddCard(String title, IQuestion question, IAnswer answer)
        {
            this.CardPack.Add(new Card() { Title = title, Question = question, Answer = answer });
        }

        public void SaceCardPack(String fileName)
        {
            using (FileStream saveFile = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            {
                // DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Model.Card>));
                // serializer.WriteObject(saveFile, this.CardPack);
                // DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Model.QuestionTextModel));
                // serializer.WriteObject(saveFile, this.CardPack[0].Question);
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Model.AnswerTextModel));
                serializer.WriteObject(saveFile, this.CardPack[0].Answer);
                // DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Model.Card));
                // serializer.WriteObject(saveFile, this.CardPack[0]);
            }
            
        }
    }
}
