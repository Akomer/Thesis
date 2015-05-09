using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class QuestionPictureModel : IQuestion
    {
        public Uri ImageSRC { get; set; }
        public String Text { get; set; }

        public QuestionPictureModel(Uri imgeSrc, String txt)
        {
            this.ImageSRC = imgeSrc;
            this.Text = txt;
        }

        public Type GetQuestionType()
        {
            return this.GetType();
        }
    }
}
