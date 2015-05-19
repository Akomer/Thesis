using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace LearningCardClasses
{
    [DataContract]
    [KnownType(typeof(AnswerLotofTextModel))]
    [KnownType(typeof(AnswerExactTextModel))]
    [KnownType(typeof(AnswerTippMixModel))]
    abstract public class IAnswer
    {
        public abstract Type GetAnswerType();
        public abstract Boolean CheckAnswer(IAnswer UserAnswer);
    }
}
