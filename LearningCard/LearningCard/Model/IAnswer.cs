using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    public interface IAnswer
    {
        Type GetQuestionType();
        Boolean CheckAnswer();
    }
}
