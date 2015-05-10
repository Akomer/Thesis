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
        private Model.Card ActualCard;
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

            this.ActualCard = this.QnAModel.GetCard();

            this.GenerateFullCard();
        }

        private void GenerateFullCard()
        {
            this.QuestionPanel = this.QuestionViewGenerator();
            this.AnswerPanel = this.AnswerViewGenerator();
        }

        private UserControl QuestionViewGenerator()
        {
            if (this.ActualCard.Question.GetQuestionType() == typeof(Model.QuestionTextModel))
            {
                UserControl v = new View.QuestionTextUserControl();
                ViewModel.QuestionTextViewModel dc = new ViewModel.QuestionTextViewModel(
                    (Model.QuestionTextModel)this.ActualCard.Question
                );
                v.DataContext = dc;
                return v;
            }
            else if (this.ActualCard.Question.GetQuestionType() == typeof(Model.QuestionPictureModel))
            {
                UserControl v = new View.QuestionPictureUserControl();
                ViewModel.QuestionPictureViewModel dc = new ViewModel.QuestionPictureViewModel(
                    (Model.QuestionPictureModel)this.ActualCard.Question
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
                    (Model.AnswerTextModel)this.ActualCard.Answer );
                v.DataContext = dc;
                return v;
            }
            return null;
        }
    }
}
