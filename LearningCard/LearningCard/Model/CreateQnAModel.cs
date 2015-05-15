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

        private CardPack CardPackItem;
        public List<Model.Card> CardPack
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
            this.CardPack = new List<Card>();
        }

        public void AddCard(String title, IQuestion question, IAnswer answer)
        {
            this.CardPack.Add(new Card() { Title = title, Question = question, Answer = answer });
        }

        public void SaveCardPack(String fileName)
        {
            // this.CardPackItem.PackName = fileName.Split('\\').Last().Split('.')[0];
            // using (FileStream saveFile = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.Write))
            // {
            //     DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
            //     serializer.WriteObject(saveFile, this.CardPackItem);
            // }
            this.CardPackItem.PackName = "TestPack";
            Model.CardPack.SaveCardPackToFile(this.CardPackItem);
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
            this.CardPack.RemoveAt(index);
        }
    }
}
