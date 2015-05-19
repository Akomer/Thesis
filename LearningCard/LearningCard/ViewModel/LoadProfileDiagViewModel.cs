using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class LoadProfileDiagViewModel : MainViewModelBase
    {
        private Int32 _ProfilList_SelectedIndex;
        public ObservableCollection<String> ProfileList
        {
            get
            {
                return new ObservableCollection<String>(Profile.ListOfAvailableProfiles());
            }
            set { }
        }
        public DelegateCommand Command_DialogLoad { get; set; }
        public DelegateCommand Command_DialogCancel { get; set; }
        public Int32 ProfilList_SelectedIndex
        {
            get
            {
                return this._ProfilList_SelectedIndex;
            }
            set
            {
                if (value != this._ProfilList_SelectedIndex)
                {
                    this._ProfilList_SelectedIndex = value;
                    this.OnPropertyChanged("ProfileList");
                }
            }
        }

        public LoadProfileDiagViewModel(View.LoadProfileDialog newDialog)
        {
            this.Command_DialogLoad = new DelegateCommand(x => this.Execute_DialogLoad(newDialog));
            this.Command_DialogCancel = new DelegateCommand(x => this.Execute_DialogCancel(newDialog));
            this.ProfilList_SelectedIndex = -1;
        }

        private void Execute_DialogLoad(View.LoadProfileDialog dialog)
        {
            dialog.DialogResult = true;
        }

        private void Execute_DialogCancel(View.LoadProfileDialog dialog)
        {
            dialog.DialogResult = false;
        }
    }
}
