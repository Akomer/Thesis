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

        public Boolean IsCheckReady
        {
            get
            {
                return this.QnAModel.QuizPhase == Model.QnAModel.AnswerPhase.CkeckAnswer;
            }
            set { }
        }
        public Boolean IsNextReady
        {
            get
            {
                return this.QnAModel.QuizPhase == Model.QnAModel.AnswerPhase.ShowAnser;
            }
            set { }
        }
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
        private String _LastAnswer;
        public String LastAnswer
        {
            get
            {
                return this._LastAnswer;
            }
            set
            {
                this._LastAnswer = value;
                this.OnPropertyChanged("LastAnswer");
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
        public DelegateCommand Command_NextCard { get; set; }

        public QnAViewModel(Model.QnAModel qModel)
        {
            this.QnAModel = qModel;

            this.Command_CheckAnswer = new DelegateCommand(x => this.Execute_CheckAnswer());
            this.Command_SkipAnswer = new DelegateCommand(x => this.Execute_SkipAnswer());
            this.Command_NextCard = new DelegateCommand(x => this.Execute_NextCard());
            this.LastAnswer = "None";
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
            if (this.ActualCard.Answer.GetAnswerType() == typeof(Model.AnswerLotofTextModel))
            {
                UserControl v = new View.AnswerTextUserControl();
                ViewModel.AnswerLotofTextViewModel dc = new ViewModel.AnswerLotofTextViewModel( 
                    (Model.AnswerLotofTextModel)this.QnAModel.UserAnswer );
                v.DataContext = dc;
                return v;
            }
            if (this.ActualCard.Answer.GetAnswerType() == typeof(Model.AnswerExactTextModel))
            {
                UserControl v = new View.AnswerTextUserControl();
                ViewModel.AnswerExactTextViewModel dc = new ViewModel.AnswerExactTextViewModel(
                    (Model.AnswerExactTextModel)this.QnAModel.UserAnswer);
                v.DataContext = dc;
                return v;
            }
            if (this.ActualCard.Answer.GetAnswerType() == typeof(Model.AnswerTippMixModel))
            {
                UserControl v = new View.UseAnswerTippMixUserControl();
                ViewModel.AnswerTippMixViewModel dc = new ViewModel.AnswerTippMixViewModel(
                    (Model.AnswerTippMixModel)this.QnAModel.UserAnswer);
                v.DataContext = dc;
                return v;
            }
            return null;
        }

        private void Execute_CheckAnswer()
        {
            if (this.QnAModel.CheckAnswer())
            {
                // System.Windows.Forms.MessageBox.Show("Right answer\nCongrat", "Right Answer",
                //     System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                this.RightAnswer();
            }
            else if (this.QnAModel.UserAnswer.GetAnswerType() == typeof(Model.AnswerLotofTextModel))
            {
                Model.AnswerLotofTextModel rightAnswerModel = (Model.AnswerLotofTextModel)this.QnAModel.GetCard().Answer;
                String rightAnswer = rightAnswerModel.Text;
                if (System.Windows.Forms.MessageBox.Show("Is your answer right?\n" + rightAnswer, "Check your answer",
                    System.Windows.Forms.MessageBoxButtons.YesNo,
                    System.Windows.Forms.MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    this.RightAnswer();
                }
                else
                {
                    this.WrongAnser();
                }
            }
            else
            {
                this.WrongAnser();
            }
            this.GenerateFullCard();
        }

        private void Execute_NextCard()
        {
            this.QnAModel.SetupNewCard();
            this.RefreshButtonState();
            this.GenerateFullCard();
        }

        private void Execute_SkipAnswer()
        {
            this.LastAnswer = "Wrong";
            this.QnAModel.AnswerWasSkipped();
            this.GenerateFullCard();
            this.RefreshButtonState();
        }

        private void WrongAnser()
        {
            this.LastAnswer = "Wrong";
            this.QnAModel.AnswerWasWrong();
            this.GenerateFullCard();
            this.RefreshButtonState();
        }

        private void RightAnswer()
        {
            this.LastAnswer = "Right";
            this.QnAModel.AnswerWasRight();
        }

        private void RefreshButtonState()
        {
            this.OnPropertyChanged("IsCheckReady");
            this.OnPropertyChanged("IsNextReady");
        }
    }
}
