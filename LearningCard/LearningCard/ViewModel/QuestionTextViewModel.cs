using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class QuestionTextViewModel : QuestionViewModelBase
    {
        private Model.QuestionTextModel QuestionModel;

        public String QuestionText 
        {
            get
            {
                return this.QuestionModel.Text;
            }
            set
            {
                this.QuestionModel.Text = value;
                OnPropertyChanged("QuestionText");
            }
        }

        public QuestionTextViewModel(Model.QuestionTextModel qModel, Boolean changeAble = true)
            : base(qModel, changeAble)
        {
            this.QuestionModel = qModel;
        }
    }
}
