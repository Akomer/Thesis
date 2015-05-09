using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class QuestionText : IQuestion
    {
        private String _Text;

        public QuestionText(String text)
        {
            this._Text = text;
        }

        public List<QuestionItem> GetQuestion()
        {
            return new List<QuestionItem>()
                {
                    new QuestionItem() { Data = (object)_Text, DataType = typeof(String)}
                };
        }
    }
}
