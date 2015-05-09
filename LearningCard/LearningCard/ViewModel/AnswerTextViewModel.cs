using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class AnswerTextViewModel : ViewModelBase
    {
        public String AnswerText
        {
            get
            {
                return this.AnswerModel.Text;
            }
            set
            {
                this.AnswerModel.Text = value;
                this.OnPropertyChanged("AnswerText");
            }
        }

        private Model.AnswerTextModel AnswerModel;

        public AnswerTextViewModel(Model.AnswerTextModel aModel)
        {
            this.AnswerModel = aModel;
        }
    }
}
