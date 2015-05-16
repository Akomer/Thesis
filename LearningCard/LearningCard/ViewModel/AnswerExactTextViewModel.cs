using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class AnswerExactTextViewModel : AnswerViewModelBase
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

        private Model.AnswerExactTextModel AnswerModel;

        public AnswerExactTextViewModel(Model.AnswerExactTextModel aModel)
            : base(aModel)
        {
            this.AnswerModel = aModel;
        }
    }
}
