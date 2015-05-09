using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class QuestionTextViewModel : ViewModelBase
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

        public QuestionTextViewModel(Model.QuestionTextModel qModel)
        {
            this.QuestionModel = qModel;
        }
    }
}
