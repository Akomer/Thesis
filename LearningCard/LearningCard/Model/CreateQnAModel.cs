using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using LearningCardClasses;

namespace LearningCard.Model
{
    class CreateQnAModel
    {

        public CardPack CardPackItem;
        public List<Card> PackOfCards
        {
            get
            {
                return this.CardPackItem.CardList;
            }
            set
            {
                this.CardPackItem.CardList = value;
            }

        }

        public CreateQnAModel()
        {
            this.CardPackItem = new CardPack();
            this.PackOfCards = new List<Card>();
        }

        public void AddCard(String title, IQuestion question, IAnswer answer)
        {
            this.PackOfCards.Add(new Card() { Title = title, Question = question, Answer = answer });
        }

        public void SaveCardPack(String fileName)
        {
            // this.CardPackItem.PackName = fileName.Split('\\').Last().Split('.')[0];
            // using (FileStream saveFile = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            // {
            //     DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
            //     serializer.WriteObject(saveFile, this.CardPackItem);
            // }
            this.CardPackItem.PackName = fileName;
            CardPack.SaveCardPackToFile(this.CardPackItem);
        }

        public void LoadCardPack(String fileName)
        {
            using (FileStream loadFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
                this.CardPackItem = (CardPack)serializer.ReadObject(loadFile);
            }
        }

        public void DeleteCard(Int32 index)
        {
            this.PackOfCards.RemoveAt(index);
        }
    }
}
