using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;

namespace LearningCard.Model
{
    class OnlineGameModel : ServiceCallBack
    {
        public enum AnswerPhase { CkeckAnswer, ShowAnser };
        public AnswerPhase QuizPhase;
        private Card _card;

        public OnlineGameModel(OnlineLearningCardService.LearningCardServiceClient client)
        {
            this._client = client;
                        this.QuizPhase = AnswerPhase.CkeckAnswer;
            this.SetupNewCard();
        }

        public Dictionary<String, int> GetScoreBoard()
        {
            return this._client.GetScoreBoard();
        }

        public Card GetCard()
        {
            if (this._card == null)
                this._card = this._client.GetCard();
            return this._card;
        }

            public IAnswer UserAnswer;
        private Int32 ActiveCardIndex;

          public void SetupNewCard()
        {
            this._card = this._client.GetCard();
            this.QuizPhase = AnswerPhase.CkeckAnswer;
            this.UserAnswer = this.UserAnswerGenerator(this._card.Answer);
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
                    newAnswer.AddEmptyTipp(item);
                }
                return (IAnswer)newAnswer;
            }
            return null;
        }

        public Boolean CheckAnswer()
        {
            return this.GetCard().Answer.CheckAnswer(this.UserAnswer);
        }

        public void AnswerWasRight()
        {
            SetupNewCard();
            this._client.SendAnswer(GlobalProfile.Instance.ActiveProfile.Name, true);
        }

        public void AnswerWasSkipped()
        {
            this.UserAnswer = this._card.Answer;
            this.QuizPhase = AnswerPhase.ShowAnser;
            this._client.SendAnswer(GlobalProfile.Instance.ActiveProfile.Name, false);
        }

        public void AnswerWasWrong()
        {
            this._client.SendAnswer(GlobalProfile.Instance.ActiveProfile.Name, false);
            if (this.UserAnswer.GetAnswerType() == typeof(AnswerLotofTextModel))
            {
                SetupNewCard();
                return;
            }
            this.UserAnswer = this._card.Answer;
            this.QuizPhase = AnswerPhase.ShowAnser;
        }

    }

}
