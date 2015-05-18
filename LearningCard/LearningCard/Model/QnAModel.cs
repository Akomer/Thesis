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
        public enum AnswerPhase { CkeckAnswer, ShowAnser };
        public AnswerPhase QuizPhase;
        private Profile UserProfile;
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
        public IAnswer UserAnswer;
        private Int32 ActiveCardIndex;
        public CardPack CardPackItem;

        public QnAModel(String fileName)
        {
            using (FileStream loadFile = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
                this.CardPackItem = (CardPack)serializer.ReadObject(loadFile);
            }
            this.SetupNewCard();
        }

        public QnAModel(CardPack cp)
        {
            this.CardPackItem = cp;
            this.QuizPhase = AnswerPhase.CkeckAnswer;
            this.SetupNewCard();
        }

        public void SetupNewCard()
        {
            this.ActiveCardIndex = this.GenerateNextCardIndex();
            // this.UserAnswer = (IAnswer)Activator.CreateInstance(this.CardPack[this.ActiveCardIndex].Answer.GetAnswerType(), 
            //                                                     new object[] { "Your answer comes here" });
            this.QuizPhase = AnswerPhase.CkeckAnswer;
            this.UserAnswer = this.UserAnswerGenerator(this.CardPack[this.ActiveCardIndex].Answer);
        }

        private IAnswer UserAnswerGenerator(IAnswer answer)
        {
            Type type = answer.GetAnswerType();
            if (type == typeof(AnswerExactTextModel))
            {
                return (IAnswer)new AnswerExactTextModel(GlobalLanguage.Instance.GetDict()["YourAnswerComesHere"]);
            }
            if (type == typeof(AnswerLotofTextModel))
            {
                return (IAnswer)new AnswerLotofTextModel(GlobalLanguage.Instance.GetDict()["YourAnswerComesHere"]);
            }
            if (type == typeof(AnswerTippMixModel))
            {
                AnswerTippMixModel oldAnswer = (AnswerTippMixModel)answer;
                AnswerTippMixModel newAnswer = new AnswerTippMixModel();
                foreach (TippMix item in oldAnswer.TippMixList)
                {
                    newAnswer.AddTipp(item.TippText);
                }
                return (IAnswer)newAnswer;
            }
            return null;
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

        public void AnswerWasSkipped()
        {
            this.UserAnswer = this.CardPack[this.ActiveCardIndex].Answer;
            this.QuizPhase = AnswerPhase.ShowAnser;
        }

        public void AnswerWasWrong()
        {
            if (this.UserAnswer.GetAnswerType() == typeof(Model.AnswerLotofTextModel))
            {
                SetupNewCard();
                return;
            }
            this.UserAnswer = this.CardPack[this.ActiveCardIndex].Answer;
            this.QuizPhase = AnswerPhase.ShowAnser;
        }

        public String CardPackName()
        {
            return this.CardPackItem.PackName;
        }
    }
}
