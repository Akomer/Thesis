using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class QuestionPicture : IQuestion
    {
        private Uri _Image;
        private String _Text;

        public QuestionPicture(Uri img, String txt)
        {
            this._Image = img;
            this._Text = txt;
        }

        public List<QuestionItem> GetQuestion()
        {
            return new List<QuestionItem>()
                {
                    new QuestionItem() { Data = (object)_Image, DataType = typeof(Uri)},
                    new QuestionItem() { Data = (object)_Text, DataType = typeof(String)}
                };
        }

        public void SetText(String txt)
        {
            this._Text = txt;
        }
    }
}
