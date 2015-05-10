using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract(Name="AnswerText")]
    class AnswerTextModel : IAnswer
    {
        [DataMember]
        public String Text { get; set; }

        public AnswerTextModel(String txt)
        {
            this.Text = txt;
        }

        public override Type GetAnswerType()
        {
            return this.GetType();
        }

        public override Boolean CheckAnswer(IAnswer UserAnwser)
        {
            AnswerTextModel uAnser = (AnswerTextModel)UserAnwser;
            return this.Text == uAnser.Text;
        }
    }
}
