using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    interface IAnswer
    {
        public String getAnswer();
        public Boolean checkAnswer();
    }
}
