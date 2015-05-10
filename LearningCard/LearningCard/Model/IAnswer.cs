using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCard.Model
{
    //[DataContract]
    public interface IAnswer
    {
        Type GetQuestionType();
        Boolean CheckAnswer();
    }
}
