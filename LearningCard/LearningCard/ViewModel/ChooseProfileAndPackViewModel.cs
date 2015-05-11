using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization.Json;

namespace LearningCard.ViewModel
{
    class ChooseProfileAndPackViewModel : MainViewModelBase
    {
        private String _ActiveProfileName;
        private Model.QnAModel QnAModel;

        public DelegateCommand Command_CreateProfile { get; set; }
        public DelegateCommand Command_LoadProfile { get; set; }
        public DelegateCommand Command_LoadCardPack { get; set; }
        public DelegateCommand Command_Start { get; set; }

        public String ActiveProfileName
        {
            get
            {
                if (this._ActiveProfileName == null)
                    return "N/A";
                else
                    return this._ActiveProfileName;
            }
            set
            {
                this._ActiveProfileName = value;
                this.OnPropertyChanged("ActiveProfileName");
            }
        }
        public String ActiveCardPack
        {
            get
            {
                if (this.QnAModel == null)
                    return "None";
                else
                    return this.QnAModel.CardPackName();
            }
            set { }
        }

        public ChooseProfileAndPackViewModel()
        {
            this.Command_CreateProfile = new DelegateCommand(x => this.Execute_CreateProfile());
            this.Command_LoadCardPack = new DelegateCommand(x => this.Execute_LoadCardPack());
            this.Command_LoadProfile = new DelegateCommand(x => this.Execute_LoadProfile());
            this.Command_Start = new DelegateCommand(x => this.Execute_Start());
        }

        private void Execute_CreateProfile()
        {
        	
        }
        private void Execute_LoadProfile()
        {
        	
        }
        private void Execute_LoadCardPack()
        {
            System.Windows.Forms.OpenFileDialog loadDialog = new System.Windows.Forms.OpenFileDialog();
            loadDialog.Filter = "Card Pack (*.lcp)|*.lcp|Any File (*.*)|*.*";
            loadDialog.Title = "Load card pack";
            if (loadDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                try
                {
                    this.QnAModel = new Model.QnAModel(loadDialog.FileName);
                }
                catch (System.Runtime.Serialization.SerializationException e)
                {
                    System.Windows.Forms.MessageBox.Show("Invalid Card Pack file", "Invalid CardPack",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
            }
            this.OnPropertyChanged("ActiveCardPack");
        }
        private void Execute_Start()
        {
            if (this.QnAModel == null)
            {
                System.Windows.Forms.MessageBox.Show("Can not start without card pack", "Missing CardPack",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            this.OnChangeMainWindowContent(typeof(View.QnAUserControl), typeof(ViewModel.QnAViewModel),
                new object[] {this.QnAModel});
        }
    }
}
