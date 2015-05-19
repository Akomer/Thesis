using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class AnswerLotofTextViewModel : AnswerViewModelBase
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

        private AnswerLotofTextModel AnswerModel;

        public AnswerLotofTextViewModel(AnswerLotofTextModel aModel) : base(aModel)
        {
            this.AnswerModel = aModel;
        }
    }
}
