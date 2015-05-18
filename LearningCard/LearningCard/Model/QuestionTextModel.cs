using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    [Serializable]
    [DataContract]
    class QuestionTextModel : IQuestion
    {
        [DataMember]
        public String Text { get; set; }

        public QuestionTextModel(String txt = "")
        {
            if (txt == "")
            {
                txt = GlobalLanguage.Instance.GetDict()["NewQuestionSimpleText"];
            }
            this.Text = txt;
        }

        public override Type GetQuestionType()
        {
            return this.GetType();
        }

    }
}
