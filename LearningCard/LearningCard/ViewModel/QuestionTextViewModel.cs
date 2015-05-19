using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class QuestionTextViewModel : QuestionViewModelBase
    {
        private QuestionTextModel QuestionModel;

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

        public QuestionTextViewModel(QuestionTextModel qModel, Boolean changeAble = true)
            : base(qModel, changeAble)
        {
            this.QuestionModel = qModel;
        }
    }
}
