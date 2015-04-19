using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearningCard.ViewModel
{
    class MainWindowViewModel : ViewModelBase
    {
        public MainWindowViewModel()
        {
            this.mainContent = new View.QnA();
            this.mainContent.DataContext = new ViewModel.QnAViewModel();

            this.OnPropertyChanged("mainContent");
        }

        public System.Windows.Controls.UserControl mainContent { get; set; }
    }
}
