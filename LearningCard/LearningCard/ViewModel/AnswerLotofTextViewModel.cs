using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        private Model.AnswerLotofTextModel AnswerModel;

        public AnswerLotofTextViewModel(Model.AnswerLotofTextModel aModel) : base(aModel)
        {
            this.AnswerModel = aModel;
        }
    }
}
