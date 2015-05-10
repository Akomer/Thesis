using System;
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
        private Model.Card ActualCard
        {
            get
            {
                return this.QnAModel.GetCard();
            }
            set{}
        }
        private UserControl _QuestionPanel;
        private UserControl _AnswerPanel;

        private Model.QnAModel QnAModel { get; set; }

        public String CardTitle
        {
            get
            {
                return this.ActualCard.Title;
            }
            set
            {

            }
        }
        public UserControl QuestionPanel
        {
            get
            {
                return this._QuestionPanel;
            }
            set
            {
                this._QuestionPanel = value;
                this.OnPropertyChanged("QuestionPanel");
            }
        }
        public UserControl AnswerPanel
        {
            get
            {
                return this._AnswerPanel;
            }
            set
            {
                this._AnswerPanel = value;
                this.OnPropertyChanged("AnswerPanel");
            }
        }
        public DelegateCommand Command_CheckAnswer { get; set; }
        public DelegateCommand Command_SkipAnswer { get; set; }

        public QnAViewModel()
        {
            while (this.QnAModel == null)
            {
                System.Windows.Forms.OpenFileDialog loadDialog = new System.Windows.Forms.OpenFileDialog();
                loadDialog.Filter = "Card Pack (*.lcp)|*.lcp|Any File (*.*)|*.*";
                loadDialog.Title = "Load card pack";
                if (loadDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    this.QnAModel = new Model.QnAModel(loadDialog.FileName);
                }
                
            }
            this.Command_CheckAnswer = new DelegateCommand(x => this.Execute_CheckAnswer());
            this.Command_SkipAnswer = new DelegateCommand(x => this.Execute_SkipAnswer());
            this.GenerateFullCard();
        }

        private void GenerateFullCard()
        {
            this.QuestionPanel = this.QuestionViewGenerator();
            this.AnswerPanel = this.AnswerViewGenerator();
            this.OnPropertyChanged("CardTitle");
        }

        private UserControl QuestionViewGenerator()
        {
            if (this.ActualCard.Question.GetQuestionType() == typeof(Model.QuestionTextModel))
            {
                UserControl v = new View.QuestionTextUserControl();
                ViewModel.QuestionTextViewModel dc = new ViewModel.QuestionTextViewModel(
                    (Model.QuestionTextModel)this.ActualCard.Question, false
                );
                v.DataContext = dc;
                return v;
            }
            else if (this.ActualCard.Question.GetQuestionType() == typeof(Model.QuestionPictureModel))
            {
                UserControl v = new View.QuestionPictureUserControl();
                ViewModel.QuestionPictureViewModel dc = new ViewModel.QuestionPictureViewModel(
                    (Model.QuestionPictureModel)this.ActualCard.Question, false
                );
                dc.EnableImageChange = true;
                v.DataContext = dc;
                return v;
            }
            return null;
        }

        private UserControl AnswerViewGenerator()
        {
            if (this.ActualCard.Answer.GetAnswerType() == typeof(Model.AnswerTextModel))
            {
                UserControl v = new View.AnswerTextUserControl();
                ViewModel.AnswerTextViewModel dc = new ViewModel.AnswerTextViewModel( 
                    (Model.AnswerTextModel)this.QnAModel.UserAnswer );
                v.DataContext = dc;
                return v;
            }
            return null;
        }

        private void Execute_CheckAnswer()
        {
            if (this.QnAModel.CheckAnswer())
            {
                System.Windows.Forms.MessageBox.Show("Right answer\nCongrat", "Right Answer",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                this.QnAModel.AnswerWasRight();
            }
            else if (this.QnAModel.UserAnswer.GetAnswerType() == typeof(Model.AnswerTextModel))
            {
                Model.AnswerTextModel rightAnswerModel = (Model.AnswerTextModel)this.QnAModel.GetCard().Answer;
                String rightAnswer = rightAnswerModel.Text;
                if (System.Windows.Forms.MessageBox.Show("Is your answer right?\n" + rightAnswer, "Check your answer",
                    System.Windows.Forms.MessageBoxButtons.YesNo,
                    System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.QnAModel.AnswerWasRight();
                }
                else
                {
                    this.QnAModel.AnswerWasWrong();
                }
            }
            else
            {
                this.QnAModel.AnswerWasWrong();
            }
            this.GenerateFullCard();
        }

        private void Execute_SkipAnswer()
        {
            this.QnAModel.AnswerWasWrong();
            this.GenerateFullCard();
        }
    }
}
