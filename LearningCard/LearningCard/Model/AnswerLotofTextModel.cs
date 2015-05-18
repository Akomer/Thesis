using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract(Name = "AnswerLotofTextModel")]
    class AnswerLotofTextModel : IAnswer
    {
        [DataMember]
        public String Text { get; set; }

        public AnswerLotofTextModel(String txt = "")
        {
            if (txt == "")
            {
                txt = GlobalLanguage.Instance.GetDict()["NewAnswerLotofText"];
            }
            this.Text = txt;
        }

        public override Type GetAnswerType()
        {
            return this.GetType();
        }

        public override Boolean CheckAnswer(IAnswer UserAnwser)
        {
            AnswerLotofTextModel uAnser = (AnswerLotofTextModel)UserAnwser;
            return this.Text == uAnser.Text;
        }
    }
}
