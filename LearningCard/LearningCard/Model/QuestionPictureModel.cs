using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [DataContract]
    class QuestionPictureModel : IQuestion
    {
        [DataMember]
        public Uri ImageSRC { get; set; }
        [DataMember]
        public String Text { get; set; }

        public QuestionPictureModel(Uri imgeSrc, String txt)
        {
            this.ImageSRC = imgeSrc;
            this.Text = txt;
        }

        public override Type GetQuestionType()
        {
            return this.GetType();
        }
    }
}
