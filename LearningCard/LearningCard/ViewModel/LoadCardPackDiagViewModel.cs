using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace LearningCard.ViewModel
{
    class LoadCardPackDiagViewModel : MainWindowViewModel
    {
        private Int32 _CardPackList_SelectedIndex;
        public ObservableCollection<String> CardPackList
        {
            get
            {
                ObservableCollection<String> obc = new ObservableCollection<string>();
                foreach (Model.CardPack item in Model.CardPack.CardPackList())
                {
                    obc.Add(item.PackName);
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

        public String GetSelectedItem()
        {
            if (CardPackList_SelectedIndex == -1)
            {
                return null;
            }
            return this.CardPackList[this.CardPackList_SelectedIndex];
        }

    }
}
