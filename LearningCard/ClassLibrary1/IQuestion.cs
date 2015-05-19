using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace LearningCardClasses
{
    [DataContract]
    [KnownType(typeof(QuestionPictureModel))]
    [KnownType(typeof(QuestionTextModel))]
    abstract public class IQuestion
    {
        public abstract Type GetQuestionType();
    }
}
