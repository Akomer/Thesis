using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.Model
{
    class GlobalProfile
    {
        private static GlobalProfile instance;

        private GlobalProfile()
        {
            this.ActiveProfile = new Profile();
        }

        public static GlobalProfile Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalProfile();
                }
                return instance;
            }
        }

        public Profile ActiveProfile { get; set; }
    }
}
