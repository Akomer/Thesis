using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class LoadCardPackDiagViewModel : MainViewModelBase
    {
        private Int32 _CardPackList_SelectedIndex;
        public ObservableCollection<String> CardPackList
        {
            get
            {
                ObservableCollection<String> obc = new ObservableCollection<string>();
                foreach (Tuple<CardPack, String> item in CardPack.CardPackList())
                {
                    obc.Add(String.Format("{0} ({1})",item.Item1.PackName, item.Item2));
                }
                return obc;
            }
            set { }
        }
        public DelegateCommand Command_DialogLoad { get; set; }
        public DelegateCommand Command_DialogCancel { get; set; }
        public Int32 CardPackList_SelectedIndex
        {
            get
            {
                return this._CardPackList_SelectedIndex;
            }
            set
            {
                if (value != this._CardPackList_SelectedIndex)
                {
                    this._CardPackList_SelectedIndex = value;
                    this.OnPropertyChanged("CardPackList");
                }
            }
        }

        public LoadCardPackDiagViewModel(View.LoadCardPackDialog newDialog)
        {
            this.Command_DialogLoad = new DelegateCommand(x => this.Execute_DialogLoad(newDialog));
            this.Command_DialogCancel = new DelegateCommand(x => this.Execute_DialogCancel(newDialog));
            this.CardPackList_SelectedIndex = -1;
        }

        private void Execute_DialogLoad(View.LoadCardPackDialog dialog)
        {
            if (this.CardPackList_SelectedIndex == -1)
            {
                dialog.DialogResult = false;
            }
            else
            {
                dialog.DialogResult = true;
            }
        }

        private void Execute_DialogCancel(View.LoadCardPackDialog dialog)
        {
            dialog.DialogResult = false;
        }

        public CardPack GetSelectedItem()
        {
            if (CardPackList_SelectedIndex == -1)
            {
                return null;
            }
            return CardPack.CardPackList()[this.CardPackList_SelectedIndex].Item1;
        }

    }
}
