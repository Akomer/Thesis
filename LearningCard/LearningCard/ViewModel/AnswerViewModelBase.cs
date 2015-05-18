using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class AnswerViewModelBase : MainViewModelBase
    {
        protected Model.IAnswer AnswerModel;

        public AnswerViewModelBase(Model.IAnswer aModel)
        {
            this.AnswerModel = aModel;
        }

        public Model.IAnswer GetModel()
        {
            return this.AnswerModel;
        }
    }
}
