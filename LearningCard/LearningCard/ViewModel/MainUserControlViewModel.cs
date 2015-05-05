﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LearningCard.ViewModel
{
    class MainUserControlViewModel : MainViewModelBase
    {
        public DelegateCommand ClickCommand_CreateNewQnA { get; private set; }

        public MainUserControlViewModel()
        {
            this.ClickCommand_CreateNewQnA = new DelegateCommand(x => this.Execute_CreateNewQnA());
        }

        private void Execute_CreateNewQnA()
        {
            this.OnChangeMainWindowContent(typeof(View.CreateQuest));
        }
    }
}