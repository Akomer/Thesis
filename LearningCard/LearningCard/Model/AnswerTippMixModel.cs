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

        public AnswerTippMixModel(Int32 TippCount = 0)
        {
            this.TippMixList = new List<TippMix>();
            for (int i = 0; i < TippCount; ++i)
            {
                this.TippMixList.Add(new TippMix() { IsChecked = false, TippText = "Tipp Text" });
            }
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

        public void AddTipp(String txt = "")
        {
            if (txt == "")
            {
                txt = GlobalLanguage.Instance.GetDict()["NewAnswerTippMixNewTipp"];
            }
            this.TippMixList.Add(new TippMix() { IsChecked = false, TippText = txt});
        }
    }
}
