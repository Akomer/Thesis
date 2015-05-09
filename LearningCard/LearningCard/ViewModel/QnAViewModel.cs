﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

using LearningCard;

namespace LearningCard.ViewModel
{
    class QnAViewModel : MainViewModelBase
    {
        public QnAViewModel()
        {
            this.QuestionUC = new View.QuestionTextUserControl();
            this.OnPropertyChanged("QuestionUC");

            this.AnswerUC = new View.AnswerTextUserControl();
            this.OnPropertyChanged("AnswerUC");
        }

        public UserControl QuestionUC { get; set; }
        public UserControl AnswerUC { get; set; }
    }
}
