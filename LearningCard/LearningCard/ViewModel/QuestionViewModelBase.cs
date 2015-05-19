using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class QuestionViewModelBase : MainViewModelBase
    {
        protected IQuestion QuestionModel;
        public Boolean isChangeable { get; set; }

        public QuestionViewModelBase(IQuestion qModel, Boolean changeAble)
        {
            this.QuestionModel = qModel;
            this.isChangeable = changeAble;
        }

        public IQuestion GetModel()
        {
            return this.QuestionModel;
        }
    }
}
