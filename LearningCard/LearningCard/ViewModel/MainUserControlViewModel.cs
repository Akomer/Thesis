using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LearningCard.ViewModel
{
    class MainUserControlViewModel : MainViewModelBase
    {
        public DelegateCommand ClickCommand_StartNewQnA { get; private set; }
        public DelegateCommand ClickCommand_CreateNewQnA { get; private set; }
        public DelegateCommand ClickCommand_StartMultiplayer { get; private set; }

        public MainUserControlViewModel()
        {
            this.ClickCommand_StartNewQnA = new DelegateCommand(x => this.Execute_StartNewQnA());
            this.ClickCommand_CreateNewQnA = new DelegateCommand(x => this.Execute_CreateNewQnA());
            this.ClickCommand_StartMultiplayer = new DelegateCommand(x => this.Execute_StartMultiplayer());
        }

        private void Execute_StartNewQnA()
        {
            //this.OnChangeMainWindowContent(typeof(View.QnAUserControl), typeof(ViewModel.QnAViewModel));
            this.OnChangeMainWindowContent(typeof(View.ChooseProfileAndPackUserControl), 
                typeof(ViewModel.ChooseProfileAndPackViewModel));
        }
        private void Execute_CreateNewQnA()
        {
            this.OnChangeMainWindowContent(typeof(View.CreateQuest), typeof(ViewModel.CreateQnAViewModel));
        }

        private void Execute_StartMultiplayer()
        {
            this.OnChangeMainWindowContent(typeof(View.OnlineLobbyUserControl), typeof(ViewModel.OnlineLobbyViewModel),
                new object[]{true});
        }

    }
}
