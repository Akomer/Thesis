using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class QuestionViewModelBase : ViewModelBase
    {
        protected Model.IQuestion qModel;

        public Model.IQuestion GetModel()
        {
            return this.qModel;
        }
    }
}
