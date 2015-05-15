using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace LearningCard.ViewModel
{
    class CreateProfileViewModel : MainViewModelBase
    {
        private Model.CreateProfileModel CPmodel;

        public String UserName
        {
            get
            {
                return this.CPmodel.UserName;
            }
            set
            {
                this.CPmodel.UserName = value;
                this.OnPropertyChanged("UserName");
            }
        }
        public ObservableCollection<String> LanguageList
        {
            get
            {
                return new ObservableCollection<string>(Model.GlobalLanguage.Instance.LanguageList());
            }
            set { }
        }
        public BitmapImage ProfilePicture
        {
            get
            {
                return this.CPmodel.ProfilPicture;
            }
            set
            {
                this.CPmodel.ProfilPicture = value;
                this.OnPropertyChanged("ProfilePicture");
            }
        }
        public DelegateCommand Command_ChangeProfilePicture { get; set; }
        public DelegateCommand Command_SaveProfile { get; set; }
        public DelegateCommand Command_LoadProfile { get; set; }

        public CreateProfileViewModel()
        {
            this.CPmodel = new Model.CreateProfileModel();
            this.Command_ChangeProfilePicture = new DelegateCommand(x => this.Execute_ChangeProfilePicture());
            this.Command_SaveProfile = new DelegateCommand(x => this.Execute_SaveProfile());
            this.Command_LoadProfile = new DelegateCommand(x => this.Execute_LoadProfile());
        }

        private void Execute_ChangeProfilePicture()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images (*.jpg, *jpeg, *.png, *.bmp)|*.jpg; *jpeg; *.png; *.bmp" ;
            dialog.Title = "Select an image file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.ProfilePicture = new BitmapImage(new Uri(dialog.FileName));
            }
        }

        private void Execute_SaveProfile()
        {
            this.CPmodel.SaveProfile();
        }

        private void Execute_LoadProfile()
        {
            View.LoadProfileDialog newDialog = new View.LoadProfileDialog();
            LoadProfileDiagViewModel diagVM = new LoadProfileDiagViewModel(newDialog);
            newDialog.DataContext = diagVM;

            if (newDialog.ShowDialog() == true && diagVM.ProfilList_SelectedIndex >= 0)
            {
                this.CPmodel.LoadProfile(diagVM.ProfileList[diagVM.ProfilList_SelectedIndex]);
                this.RefreshView();
            }
        }

        private void RefreshView()
        {
            this.OnPropertyChanged("UserName");
            this.OnPropertyChanged("ProfilePicture");
        }
    }
}
