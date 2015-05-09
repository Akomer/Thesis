using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class AnswerTextModel : IAnswer
    {
        public String Text { get; set; }

        public AnswerTextModel(String txt)
        {
            this.Text = txt;
        }

        public Type GetQuestionType()
        {
            return this.GetType();
        }

        public Boolean CheckAnswer()
        {
            return false;
        }
    }
}
