using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract]
    class AnswerTippMixModel : IAnswer
    {
        [DataMember]
        public List<TippMix> TippMixList { get; set; }

        public AnswerTippMixModel()
        {
            this.TippMixList = new List<TippMix>();
        }

        public override Type GetAnswerType()
        {
            return this.GetType();
        }

        public override bool CheckAnswer(IAnswer UserAnswer)
        {
            AnswerTippMixModel UAnswer = (AnswerTippMixModel)UserAnswer;
            for (int i = 0; i < this.TippMixList.Count; ++i)
            {
                if (this.TippMixList[i].IsChecked != UAnswer.TippMixList[i].IsChecked)
                    return false;
            }
            return true;
        }

        public void AddTipp(String txt = "New Tipp")
        {
            this.TippMixList.Add(new TippMix() { IsChecked = false, TippText = txt});
        }
    }
}
