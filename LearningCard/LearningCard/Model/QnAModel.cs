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
        private Profile UserProfile;
        private CardPack CardPackItem;
        private List<Model.Card> CardPack
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
        public IAnswer UserAnswer;
        private Int32 ActiveCardIndex;


        public QnAModel(String fileName)
        {
            using (FileStream loadFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
                this.CardPackItem = (CardPack)serializer.ReadObject(loadFile);
            }
            this.SetupNewCard();
        }

        public void SetupNewCard()
        {
            this.ActiveCardIndex = this.GenerateNextCardIndex();
            this.UserAnswer = (IAnswer)Activator.CreateInstance(this.CardPack[this.ActiveCardIndex].Answer.GetAnswerType(), 
                                                                new object[] { "Your answer comes here" });
        }

        private Int32 GenerateNextCardIndex()
        {
            Random r = new Random();
            return r.Next(this.CardPack.Count);
        }

        public Card GetCard()
        {
            return this.CardPack[this.ActiveCardIndex];
        }

        public Boolean CheckAnswer()
        {
            return this.GetCard().Answer.CheckAnswer(this.UserAnswer);
        }

        public void AnswerWasRight()
        {
            SetupNewCard();
        }

        public void AnswerWasWrong()
        {
            SetupNewCard();
        }

        public String CardPackName()
        {
            return this.CardPackItem.PackName;
        }
    }
}
