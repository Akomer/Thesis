using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class QuestionTextModel : IQuestion
    {
        public String Text { get; set; }

        public QuestionTextModel(String txt)
        {
            this.Text = txt;
        }

        public Type GetQuestionType()
        {
            return this.GetType();
        }

    }
}
