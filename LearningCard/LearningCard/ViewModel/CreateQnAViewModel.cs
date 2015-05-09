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
        private Int32 _Card_SelectedIndex;
        private String _CardTitle;
        private Model.CreateQnAModel QnAModel { get; set; }

        public String CardTitle 
        { 
            get {return this._CardTitle;}
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
        public UserControl QuestionPanel {get; set; }
        private List<UserControl> QuestionPanelList;
        public UserControl AnswerPanel { get; set; }
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
                // this.OnPropertyChanged("QuestionType_SelectedIndex");
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
                // this.OnPropertyChanged("QuestionType_SelectedIndex");
            }
        }
        public DelegateCommand Command_AddCard { get; set; }

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

            // this.CardTitleList = new ObservableCollection<String>();
            // this.CardTitleList.Add("First title");
            // this.CardTitleList.Add("Title 2.");
            // this.OnPropertyChanged("CardTitleList");

            // this.QuestionPanel = new View.QuestionUserControl();
            // this.OnPropertyChanged("QuestionPanel");
            this.QuestionPanelList = new List<UserControl>() {null, null };

            this.AnswerPanel = new View.AnswerUserControl();
            this.OnPropertyChanged("AnswerPanel");

            this.Command_AddCard = new DelegateCommand(x => this.Execute_AddCard());

            this._QuestionType_SelectedIndex = -1;
        }

        public void QuestionType_SelectionChanged()
        {
            if (this.QuestionPanelList[this.QuestionType_SelectedIndex] == null)
            {
                this.QuestionPanelList[this.QuestionType_SelectedIndex] = this.QuestinViewGenerator(QuestionType_SelectedIndex);
            }
            this.QuestionPanel = this.QuestionPanelList[this.QuestionType_SelectedIndex];
            this.OnPropertyChanged("QuestionPanel");
        }

        private void Card_SelectionChanged()
        {
            if (this._Card_SelectedIndex == this.QnAModel.CardPack.Count)
            {
                this.CardTitle = "New Card";
            }
            else
            {
                this.CardTitle = this.QnAModel.CardPack[this._Card_SelectedIndex].Title;
            }
        }

        private UserControl QuestinViewGenerator(Int32 t)
        {
            if (t == 0) return new View.QuestionUserControl();
            if (t == 1)
            {
                UserControl v = new View.QuestionPictureUserControl();
                ViewModel.QuestionPictureViewModel dc = new ViewModel.QuestionPictureViewModel();
                dc.EnableImageChange = true;
                v.DataContext = dc;
                return v;
            }
            return null;
        }

        private void Execute_AddCard()
        {
            this.QnAModel.AddCard(this.CardTitle);
            this.OnPropertyChanged("CardTitleList");
        }
    }
}
