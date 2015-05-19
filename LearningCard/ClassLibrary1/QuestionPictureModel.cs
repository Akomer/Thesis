using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;

namespace LearningCardClasses
{
    [DataContract]
    public class QuestionPictureModel : IQuestion
    {
        [DataMember]
        private Uri _ImageSRC { get; set; }
        [DataMember]
        public String Text { get; set; }

        public Uri ImageSRC
        {
            get
            {
                return _ImageSRC;
            }
            set
            {
                this._ImageSRC = value;
            }
        }

        public Uri GetImageFullPath()
        {
            if (this.ImageSRC.IsAbsoluteUri)
                return this.ImageSRC;
            String BasePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            return new Uri(BasePath + this.ImageSRC);
        }

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
