using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace LearningCard.ViewModel
{
    class CreateQnAViewModel : MainViewModelBase
    {
        private Int32 _QuestionType_SelectedIndex;
        private Int32 _AnswerType_SelectedIndex;
        private Int32 _Card_SelectedIndex;
        private String _CardTitle;
        private UserControl _QuestionPanel;
        private UserControl _AnswerPanel;
        private String _AddModifyButtonText;
        private Model.CreateQnAModel QnAModel { get; set; }

        public String CardTitle 
        { 
            get 
            {
                return this._CardTitle;
            }
            set 
            { 
                this._CardTitle = value;
                this.OnPropertyChanged("CardTitle");
            }
        }
        public ObservableCollection<String> QuestionTypeList { get; set; }
        public ObservableCollection<String> AnswerTypeList { get; set; }
        public ObservableCollection<Model.Card> CardTitleList 
        {
            get
            {
                ObservableCollection<Model.Card>  obsC = new ObservableCollection<Model.Card>(this.QnAModel.CardPack);
                obsC.Add(new Model.Card() { Title = "<New Card>" });
                return obsC;
            }
            set { }
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
        private List<UserControl> QuestionPanelList;
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
        private List<UserControl> AnswerPanelList;
        public Int32 QuestionType_SelectedIndex
        {
            get { return _QuestionType_SelectedIndex; }

            set
            {
                if (_QuestionType_SelectedIndex == value)
                {
                    return;
                }

                _QuestionType_SelectedIndex = value;

                this.QuestionType_SelectionChanged();
                this.OnPropertyChanged("QuestionType_SelectedIndex");
            }
        }
        public Int32 AnswerType_SelectedIndex
        {
            get { return _AnswerType_SelectedIndex; }

            set
            {
                if (_AnswerType_SelectedIndex == value)
                {
                    return;
                }

                _AnswerType_SelectedIndex = value;

                this.AnswerType_SelectionChanged();
                this.OnPropertyChanged("AnswerType_SelectedIndex");
            }
        }
        public Int32 Card_SelectedIndex
        {
            get { return _Card_SelectedIndex; }

            set
            {
                if (_Card_SelectedIndex == value)
                {
                    return;
                }
                _Card_SelectedIndex = value;

                this.Card_SelectionChanged();
            }
        }
        public DelegateCommand Command_AddCard { get; set; }
        public DelegateCommand Command_SaveCardPack { get; set; }
        public DelegateCommand Command_LoadCardPack { get; set; }
        public DelegateCommand Command_DeleteCard { get; set; }
        public String AddModifyButtonText 
        {
            get
            {
                return this._AddModifyButtonText;
            }
            set
            {
                this._AddModifyButtonText = value;
                this.OnPropertyChanged("AddModifyButtonText");
            }
        }

        public CreateQnAViewModel()
        {
            this.QnAModel = new Model.CreateQnAModel();

            this.QuestionTypeList = new ObservableCollection<String>();
            this.QuestionTypeList.Add("Simple Text");
            this.QuestionTypeList.Add("Picture and text");
            this.OnPropertyChanged("QuestionTypeList");

            this.AnswerTypeList = new ObservableCollection<String>();
            this.AnswerTypeList.Add("Simple text");
            this.AnswerTypeList.Add("Chocies");
            this.OnPropertyChanged("AnswerTypeList");

            this.GenerateNewCardParts();

            this.Command_AddCard = new DelegateCommand(x => this.Execute_AddCard());
            this.Command_SaveCardPack = new DelegateCommand(x => this.Execute_SaceCardPack());
            this.Command_LoadCardPack = new DelegateCommand(x => this.Execute_LoadCardPack());
            this.Command_DeleteCard = new DelegateCommand(x => this.Execute_DeleteCard());

            this._Card_SelectedIndex = -1;
            this.AddModifyButtonText = "Add Card";
        }

        private void QuestionType_SelectionChanged()
        {
            if (this.QuestionType_SelectedIndex == -1)
            {
                return;
            }
            if (this.QuestionPanelList[this.QuestionType_SelectedIndex] == null)
            {
                this.QuestionPanelList[this.QuestionType_SelectedIndex] = this.QuestionViewGenerator(QuestionType_SelectedIndex);
            }
            this.QuestionPanel = this.QuestionPanelList[this.QuestionType_SelectedIndex];
            // this.OnPropertyChanged("QuestionPanel");
        }

        private void AnswerType_SelectionChanged()
        {
            if (this.AnswerType_SelectedIndex == -1)
            {
                return;
            }
            if (this.AnswerPanelList[this.AnswerType_SelectedIndex] == null)
            {
                this.AnswerPanelList[this.AnswerType_SelectedIndex] = this.AnswerViewGenerator(AnswerType_SelectedIndex);
            }
            this.AnswerPanel = this.AnswerPanelList[this.AnswerType_SelectedIndex];
            // this.OnPropertyChanged("AnswerPanel");
        }        

        private void Card_SelectionChanged()
        {
            if (this.IsNewCard())
            {
                this.AddModifyButtonText = "Add Card";
                this.CardTitle = null;
                this.QuestionType_SelectedIndex = 0;
                this.AnswerType_SelectedIndex = 0;
                this.QuestionPanel = this.QuestionPanelList[this.QuestionType_SelectedIndex];
                this.AnswerPanel = this.AnswerPanelList[this.AnswerType_SelectedIndex];
            }
            else if (this.Card_SelectedIndex == -1)
            {
                return;
            }
            else
            {
                this.AddModifyButtonText = "Modify Card Title";
                this.CardTitle = this.QnAModel.CardPack[this.Card_SelectedIndex].Title;
                UserControl qView = null;
                if (this.QnAModel.CardPack[this.Card_SelectedIndex].Question.GetType().Name == "QuestionTextModel")
                {
                    qView = new View.QuestionTextUserControl();
                    ViewModel.QuestionTextViewModel dc = new ViewModel.QuestionTextViewModel(
                        (Model.QuestionTextModel)this.QnAModel.CardPack[this.Card_SelectedIndex].Question);
                    qView.DataContext = dc;
                }
                else if (this.QnAModel.CardPack[this.Card_SelectedIndex].Question.GetType().Name == "QuestionPictureModel")
                {
                    qView = new View.QuestionPictureUserControl();
                    ViewModel.QuestionPictureViewModel dc = new ViewModel.QuestionPictureViewModel(
                        (Model.QuestionPictureModel)this.QnAModel.CardPack[this.Card_SelectedIndex].Question);
                    qView.DataContext = dc;
                }
                this.QuestionPanel = qView;
                // this.OnPropertyChanged("QuestionPanel");

                UserControl aView = null;
                if (this.QnAModel.CardPack[this.Card_SelectedIndex].Answer.GetType().Name == "AnswerTextModel")
                {
                    aView = new View.AnswerTextUserControl();
                    ViewModel.AnswerTextViewModel dc = new ViewModel.AnswerTextViewModel(
                        (Model.AnswerTextModel)this.QnAModel.CardPack[this.Card_SelectedIndex].Answer);
                    aView.DataContext = dc;
                }
                this.AnswerPanel = aView;
                // this.OnPropertyChanged("AnswerPanel");
            }
        }

        private UserControl QuestionViewGenerator(Int32 t)
        {
            if (t == 0)
            {
                UserControl v = new View.QuestionTextUserControl();
                ViewModel.QuestionTextViewModel dc = new ViewModel.QuestionTextViewModel(
                    new Model.QuestionTextModel("New Question - text")
                );
                v.DataContext = dc;
                return v;
            }
            if (t == 1)
            {
                UserControl v = new View.QuestionPictureUserControl();
                ViewModel.QuestionPictureViewModel dc = new ViewModel.QuestionPictureViewModel(
                    new Model.QuestionPictureModel(new Uri(@"/Images/question_mark.png", UriKind.Relative), "Title of picture Q." )
                );
                dc.EnableImageChange = true;
                v.DataContext = dc;
                return v;
            }
            return null;
        }

        private UserControl AnswerViewGenerator(Int32 t)
        {
            if (t == 0)
            {
                UserControl v = new View.AnswerTextUserControl();
                ViewModel.AnswerTextViewModel dc = new ViewModel.AnswerTextViewModel(
                    new Model.AnswerTextModel("New Answer - text")
                );
                v.DataContext = dc;
                return v;
            }
            return null;
        }

        private void Execute_AddCard()
        {
            if(this.CardTitle == "" || this.CardTitle == null)
            {
                System.Windows.Forms.MessageBox.Show("Can not add a card without title", "Missing Title",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            if (this.QuestionPanel == null)
            {
                System.Windows.Forms.MessageBox.Show("Can not add a card without Question", "Missing Question",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            if (this.AnswerPanel == null)
            {
                System.Windows.Forms.MessageBox.Show("Can not add a card without Answer", "Missing Answer",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            if (this.IsNewCard() || this.Card_SelectedIndex == -1)
            {
                QuestionViewModelBase qVMB = (QuestionViewModelBase)this.QuestionPanelList[this.QuestionType_SelectedIndex].DataContext;
                Model.IQuestion qModel = qVMB.GetModel();
                AnswerViewModelBase aVMB = (AnswerViewModelBase)this.AnswerPanelList[this.AnswerType_SelectedIndex].DataContext;
                Model.IAnswer aModel = aVMB.GetModel();
                this.QnAModel.AddCard(this.CardTitle, qModel, aModel);
                this.ClearAfterNewCardAdded();
            }
            else
            {
                this.QnAModel.CardPack[this.Card_SelectedIndex].Title = this.CardTitle;
                //this.QnAModel.CardPack[this.Card_SelectedIndex].Question 
            }
            this.OnPropertyChanged("CardTitleList");
            
        }

        private void Execute_SaceCardPack()
        {
            System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();
            saveDialog.Filter = "Card Pack (*.lcp)|*.lcp|Any File (*.*)|*.*";
            saveDialog.Title = "Save new card pack";
            if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.QnAModel.SaveCardPack(saveDialog.FileName);
            }
        }

        private void Execute_LoadCardPack()
        {
            System.Windows.Forms.OpenFileDialog loadDialog = new System.Windows.Forms.OpenFileDialog();
            loadDialog.Filter = "Card Pack (*.lcp)|*.lcp|Any File (*.*)|*.*";
            loadDialog.Title = "Load card pack";
            if (loadDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                this.QnAModel.LoadCardPack(loadDialog.FileName);
                this.OnPropertyChanged("CardTitleList");
                this.Card_SelectedIndex = -1;
            }
        }

        private void Execute_DeleteCard()
        {
            if (this.Card_SelectedIndex < this.QnAModel.CardPack.Count && this.Card_SelectedIndex >= 0)
            {
                this.QnAModel.DeleteCard(this.Card_SelectedIndex);
                this.OnPropertyChanged("CardTitleList");
            }
        }

        private void ClearAfterNewCardAdded()
        {
            this.GenerateNewCardParts();
            this.CardTitle = null;
            this.QuestionPanel = null;
            this.AnswerPanel = null;
        }

        private void GenerateNewCardParts()
        {
            this.QuestionPanelList = new List<UserControl>();
            foreach (String item in this.QuestionTypeList)
            {
                this.QuestionPanelList.Add(null);
            }
            this.AnswerPanelList = new List<UserControl>();
            foreach (String item in this.AnswerTypeList)
            {
                this.AnswerPanelList.Add(null);
            }
            this.QuestionType_SelectedIndex = -1;
            this.AnswerType_SelectedIndex = -1;
        }

        private Boolean IsNewCard()
        {
            return this.Card_SelectedIndex == this.QnAModel.CardPack.Count;
        }
    }
}
