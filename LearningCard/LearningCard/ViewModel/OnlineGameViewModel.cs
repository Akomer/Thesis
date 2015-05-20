using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using LearningCardClasses;

namespace LearningCard.ViewModel
{
    class OnlineGameViewModel : MainWindowViewModel
    {
        private Card ActualCard
        {
            get
            {
                return this.GameModel.GetCard();
            }
            set{}
        }
        private UserControl _QuestionPanel;
        private UserControl _AnswerPanel;

        private Model.OnlineGameModel GameModel { get; set; }

        public Boolean IsCheckReady
        {
            get
            {
                return this.GameModel.QuizPhase == Model.OnlineGameModel.AnswerPhase.CkeckAnswer;
            }
            set { }
        }
        public Boolean IsNextReady
        {
            get
            {
                return this.GameModel.QuizPhase == Model.OnlineGameModel.AnswerPhase.ShowAnser;
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
        public ObservableCollection<Tuple<String, int>> ScoreBoard
        {
            get
            {
                ObservableCollection<Tuple<String, int>> obc = new ObservableCollection<Tuple<string,int>>();
                foreach(var item in GameModel.GetScoreBoard())
                {
                    obc.Add(new Tuple<string,int>(item.Key, item.Value));
                }
                return obc;
            }
            set { }
        }

        public OnlineGameViewModel(Model.OnlineGameModel gModel)
        {
            
            this.GameModel = gModel;

            this.Command_CheckAnswer = new DelegateCommand(x => this.Execute_CheckAnswer());
            this.Command_SkipAnswer = new DelegateCommand(x => this.Execute_SkipAnswer());
            this.Command_NextCard = new DelegateCommand(x => this.Execute_NextCard());
            this.LastAnswer = "None";
            this.GenerateFullCard();
        }

        private void GenerateFullCard()
        {
            this.OnPropertyChanged("ScoreBoard");
            this.QuestionPanel = this.QuestionViewGenerator();
            this.AnswerPanel = this.AnswerViewGenerator();
            this.OnPropertyChanged("CardTitle");
        }

        private UserControl QuestionViewGenerator()
        {
            if (this.ActualCard.Question.GetQuestionType() == typeof(QuestionTextModel))
            {
                UserControl v = new View.QuestionTextUserControl();
                ViewModel.QuestionTextViewModel dc = new ViewModel.QuestionTextViewModel(
                    (QuestionTextModel)this.ActualCard.Question, false
                );
                v.DataContext = dc;
                return v;
            }
            else if (this.ActualCard.Question.GetQuestionType() == typeof(QuestionPictureModel))
            {
                UserControl v = new View.QuestionPictureUserControl();
                ViewModel.QuestionPictureViewModel dc = new ViewModel.QuestionPictureViewModel(
                    (QuestionPictureModel)this.ActualCard.Question, false
                );
                dc.EnableImageChange = true;
                v.DataContext = dc;
                return v;
            }
            return null;
        }

        private UserControl AnswerViewGenerator()
        {
            if (this.ActualCard.Answer.GetAnswerType() == typeof(AnswerLotofTextModel))
            {
                UserControl v = new View.AnswerTextUserControl();
                ViewModel.AnswerLotofTextViewModel dc = new ViewModel.AnswerLotofTextViewModel( 
                    (AnswerLotofTextModel)this.GameModel.UserAnswer );
                v.DataContext = dc;
                return v;
            }
            if (this.ActualCard.Answer.GetAnswerType() == typeof(AnswerExactTextModel))
            {
                UserControl v = new View.AnswerTextUserControl();
                ViewModel.AnswerExactTextViewModel dc = new ViewModel.AnswerExactTextViewModel(
                    (AnswerExactTextModel)this.GameModel.UserAnswer);
                v.DataContext = dc;
                return v;
            }
            if (this.ActualCard.Answer.GetAnswerType() == typeof(AnswerTippMixModel))
            {
                UserControl v = new View.UseAnswerTippMixUserControl();
                ViewModel.AnswerTippMixViewModel dc = new ViewModel.AnswerTippMixViewModel(
                    (AnswerTippMixModel)this.GameModel.UserAnswer);
                v.DataContext = dc;
                return v;
            }
            return null;
        }

        private void Execute_CheckAnswer()
        {
            if (this.GameModel.CheckAnswer())
            {
                // System.Windows.Forms.MessageBox.Show("Right answer\nCongrat", "Right Answer",
                //     System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.None);
                this.RightAnswer();
            }
            else if (this.GameModel.UserAnswer.GetAnswerType() == typeof(AnswerLotofTextModel))
            {
                AnswerLotofTextModel rightAnswerModel = (AnswerLotofTextModel)this.GameModel.GetCard().Answer;
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
            this.GameModel.SetupNewCard();
            this.RefreshButtonState();
            this.GenerateFullCard();
        }

        private void Execute_SkipAnswer()
        {
            this.LastAnswer = "Wrong";
            this.GameModel.AnswerWasSkipped();
            this.GenerateFullCard();
            this.RefreshButtonState();
        }

        private void WrongAnser()
        {
            this.LastAnswer = "Wrong";
            this.GameModel.AnswerWasWrong();
            this.GenerateFullCard();
            this.RefreshButtonState();
        }

        private void RightAnswer()
        {
            this.LastAnswer = "Right";
            this.GameModel.AnswerWasRight();
        }

        private void RefreshButtonState()
        {
            this.OnPropertyChanged("IsCheckReady");
            this.OnPropertyChanged("IsNextReady");
        }
    }
}
