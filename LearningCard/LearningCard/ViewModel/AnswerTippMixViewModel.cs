using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class AnswerTippMixViewModel : AnswerViewModelBase
    {
        private AnswerTippMixModel aModel;
        private ObservableCollection<TippMix> _TippMixList;

        public DelegateCommand Command_AddNewTipp { get; set; }
        public ObservableCollection<TippMix> TippMixList
        {
            get
            {
                this._TippMixList = new ObservableCollection<TippMix>(this.aModel.TippMixList);
                return this._TippMixList;
            }
            set
            {
                this._TippMixList = value;
                this.aModel.TippMixList = new List<TippMix>(this._TippMixList);
                this.OnPropertyChanged("TippMixList");
            }
        }

        public AnswerTippMixViewModel(AnswerTippMixModel answerTippMixModel)
            : base(answerTippMixModel)
        {
            this.aModel = answerTippMixModel;
            this.Command_AddNewTipp = new DelegateCommand(x => this.Execute_AddNewTipp());
        }

        private void Execute_AddNewTipp()
        {
            this.aModel.AddTipp(Model.LanguageFactory.DefaultTippMix());
            this.OnPropertyChanged("TippMixList");
        }
    }
}
