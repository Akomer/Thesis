using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class QuestionViewModelBase : ViewModelBase
    {
        protected Model.IQuestion QuestionModel;
        public Boolean isChangeable { get; set; }

        public QuestionViewModelBase(Model.IQuestion qModel, Boolean changeAble)
        {
            this.QuestionModel = qModel;
            this.isChangeable = changeAble;
        }

        public Model.IQuestion GetModel()
        {
            return this.QuestionModel;
        }
    }
}
