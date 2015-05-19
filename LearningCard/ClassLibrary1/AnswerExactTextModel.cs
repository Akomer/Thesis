using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCardClasses
{   
    [DataContract]
    public class AnswerExactTextModel : IAnswer
    {
        [DataMember]
        public String Text { get; set; }

        public AnswerExactTextModel(String txt)
        {
            this.Text = txt;
        }

        public override Type GetAnswerType()
        {
            return this.GetType();
        }

        public override Boolean CheckAnswer(IAnswer UserAnwser)
        {
            AnswerExactTextModel uAnser = (AnswerExactTextModel)UserAnwser;
            return this.Text == uAnser.Text;
        }
    }
    
}
