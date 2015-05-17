using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LearningCard.ViewModel
{
    class AnswerTippMixViewModel : AnswerViewModelBase
    {
        private Model.AnswerTippMixModel aModel;
        private ObservableCollection<Model.TippMix> _TippMixList;

        public DelegateCommand Command_AddNewTipp { get; set; }
        public ObservableCollection<Model.TippMix> TippMixList
        {
            get
            {
                this._TippMixList = new ObservableCollection<Model.TippMix>(this.aModel.TippMixList);
                return this._TippMixList;
            }
            set
            {
                this._TippMixList = value;
                this.aModel.TippMixList = new List<Model.TippMix>(this._TippMixList);
                this.OnPropertyChanged("TippMixList");
            }
        }

        public AnswerTippMixViewModel(Model.AnswerTippMixModel answerTippMixModel)
            : base(answerTippMixModel)
        {
            this.aModel = answerTippMixModel;
            this.Command_AddNewTipp = new DelegateCommand(x => this.Execute_AddNewTipp());
        }

        private void Execute_AddNewTipp()
        {
            this.aModel.AddTipp();
            this.OnPropertyChanged("TippMixList");
        }
    }
}
