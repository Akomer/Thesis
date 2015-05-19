using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;

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

        private AnswerExactTextModel AnswerModel;

        public AnswerExactTextViewModel(AnswerExactTextModel aModel)
            : base(aModel)
        {
            this.AnswerModel = aModel;
        }
    }
}
