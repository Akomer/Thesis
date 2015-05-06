using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LearningCard.ViewModel
{
    class CreateQnAViewModel : MainViewModelBase
    {
        public List<String> QuestionTypeList { get; set; }
        public List<String> AnswerTypeList { get; set; }
        public UserControl QuestionPanel {get; set; }
        public UserControl AnswerPanel { get; set; }

        public CreateQnAViewModel()
        {
            this.QuestionTypeList = new List<String>();
            this.QuestionTypeList.Add("Simple Text");
            this.QuestionTypeList.Add("Picture and text");
            this.OnPropertyChanged("QuestionTypeList");

            this.AnswerTypeList = new List<String>();
            this.AnswerTypeList.Add("Simple text");
            this.AnswerTypeList.Add("Chocies");
            this.OnPropertyChanged("AnswerTypeList");

            this.QuestionPanel = new View.QuestionUserControl();
            this.OnPropertyChanged("QuestionPanel");

            this.AnswerPanel = new View.AnswerUserControl();
            this.OnPropertyChanged("AnswerPanel");
        }
    }
}
