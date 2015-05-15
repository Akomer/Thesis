using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.OnlineLearningCardService
{
    class PartialClientClasses
    {

    }

    partial class Profile
    {
        public Profile()
        {
            this.Name = "Guest";
            this.StatisticData = new Dictionary<string, int[]>();
        }

    }
}
