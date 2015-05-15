using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LearningCard.ViewModel
{
    class MainWindowViewModel : MainViewModelBase
    {
        private UserControl _mainContent;
        private ViewModel.MainViewModelBase mainContentViewModel;

        public DelegateCommand Command_ChangeLanguage { get; set; }
        public ObservableCollection<String> LanguageList
        {
            get
            {
                return new ObservableCollection<String>(Model.GlobalLanguage.Instance.LanguageList());
            }
            set { }
        }

        public UserControl mainContent
        {
            get { return this._mainContent; }
            set
            {
                this._mainContent = value;
                this.OnPropertyChanged("mainContent");
            }
        }

        public MainWindowViewModel()
        {
            // this.mainContent = new View.QnA();
            this.mainContent = new View.MainUserControl();
            this.mainContentViewModel = new ViewModel.MainUserControlViewModel();
            this.mainContent.DataContext = this.mainContentViewModel;
            this.Command_ChangeLanguage = new DelegateCommand(x => this.Execute_ChangeLanguage((int)x));
            // this.mainContent.DataContext = new ViewModel.QnAViewModel();

            this.mainContentViewModel.ChangeMainWindowContent += new ViewModel.Event_mainControlChange(VM_ChangeMainWindow);
        }

        private void VM_ChangeMainWindow(ViewModel.MainControlChangeEventArgs _args)
        {
            this.mainContent = (UserControl)Activator.CreateInstance(_args.NewUserControl);
            this.mainContentViewModel = (ViewModel.MainViewModelBase)Activator.CreateInstance(_args.NewViewModel, _args.args);
            this.mainContent.DataContext = this.mainContentViewModel;
            this.mainContentViewModel.ChangeMainWindowContent += new ViewModel.Event_mainControlChange(VM_ChangeMainWindow);
        }

        private void Execute_ChangeLanguage(Int32 index)
        {
            Model.GlobalLanguage.Instance.SetLanguage("hun");
            this.RefreshLanguage();
            this.mainContentViewModel.RefreshLanguage();
        }
    }
}
