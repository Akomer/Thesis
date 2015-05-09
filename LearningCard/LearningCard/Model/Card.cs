using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class Card
    {
        public IAnswer Answer { get; set; }
        public IQuestion Question { get; set; }

        public String Title { get; set; }
    }
}
