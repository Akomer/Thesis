using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LearningCard.ViewModel
{
    class MainWindowViewModel : MainViewModelBase
    {
        private UserControl _mainContent;
        private ViewModel.MainViewModelBase mainContentViewModel; 
        
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
            // this.mainContent.DataContext = new ViewModel.QnAViewModel();

            this.mainContentViewModel.ChangeMainWindowContent += new ViewModel.MyEventDelegate(VM_ChangeMainWindow);
        }

        private void VM_ChangeMainWindow(ViewModel.ViewModelChangeEventArgs _args)
        {
            this.mainContent = (UserControl)Activator.CreateInstance(_args.Data);
        }
    }
}
