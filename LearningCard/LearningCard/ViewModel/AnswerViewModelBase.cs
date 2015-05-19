using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class AnswerViewModelBase : MainViewModelBase
    {
        protected IAnswer AnswerModel;

        public AnswerViewModelBase(IAnswer aModel)
        {
            this.AnswerModel = aModel;
        }

        public IAnswer GetModel()
        {
            return this.AnswerModel;
        }
    }
}
