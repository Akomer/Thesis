﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Controls;

namespace LearningCard.ViewModel
{
    class MainWindowViewModel : MainViewModelBase
    {
        private UserControl _mainContent;
        private ViewModel.MainViewModelBase mainContentViewModel;

        public DelegateCommand Command_ChangeLanguage { get; set; }
        public DelegateCommand Command_NewProfile { get; set; }
        public DelegateCommand Command_LoadActiveProfile { get; set; }  
        public DelegateCommand Command_ExportCardPack { get; set; }  
        public DelegateCommand Command_ImportCardPack { get; set; }  
        
        public ObservableCollection<String> LanguageList
        {
            get
            {
                return new ObservableCollection<String>(Model.GlobalLanguage.Instance.LanguageList());
            }
            set { }
        }
        public Model.Profile ActiveProfile
        {
            get
            {
                return Model.GlobalProfile.Instance.ActiveProfile;
            }
            set
            {
                Model.GlobalProfile.Instance.ActiveProfile = value;
                this.OnPropertyChanged("ActiveProfile");
            }
        }

        public UserControl mainContent
        {
            get { return this._mainContent; }
            set
            {
                this._mainContent = value;
                this.OnPropertyChanged("mainContent");
            }
        }

        public MainWindowViewModel()
        {
            // this.mainContent = new View.QnA();
            this.mainContent = new View.MainUserControl();
            this.mainContentViewModel = new ViewModel.MainUserControlViewModel();
            this.mainContent.DataContext = this.mainContentViewModel;
            this.Command_ChangeLanguage = new DelegateCommand(x => this.Execute_ChangeLanguage((int)x));
            this.Command_NewProfile = new DelegateCommand(x => this.VM_ChangeMainWindow(
                new ViewModel.MainControlChangeEventArgs(typeof(View.CreateProfileUserControl), typeof(ViewModel.CreateProfileViewModel))));
            this.Command_LoadActiveProfile = new DelegateCommand(x => this.Execut_LoadActiveProfile());
            this.Command_ExportCardPack = new DelegateCommand(x => this.Execute_ExportCardPack());
            this.Command_ImportCardPack = new DelegateCommand(x => this.Execute_ImportCardPack());

            this.mainContentViewModel.ChangeMainWindowContent += new ViewModel.Event_mainControlChange(VM_ChangeMainWindow);
        }

        private void VM_ChangeMainWindow(ViewModel.MainControlChangeEventArgs _args)
        {
            ViewModel.MainViewModelBase newViewModelBase;
            newViewModelBase = (ViewModel.MainViewModelBase)Activator.CreateInstance(_args.NewViewModel, _args.args);
            // newViewModelBase = (ViewModel.MainViewModelBase)Activator.CreateInstance(_args.NewViewModel);
            if (!newViewModelBase.IsReady())
            {
                return;
            }
            UserControl newUserControl = (UserControl)Activator.CreateInstance(_args.NewUserControl);
            this.mainContent = newUserControl;
            this.mainContentViewModel = newViewModelBase;
            this.mainContent.DataContext = this.mainContentViewModel;
            this.mainContentViewModel.ChangeMainWindowContent += new ViewModel.Event_mainControlChange(VM_ChangeMainWindow);
        }

        private void Execute_ChangeLanguage(Int32 index)
        {
            Model.GlobalLanguage.Instance.SetLanguage("hun");
            this.RefreshLanguage();
            this.mainContentViewModel.RefreshLanguage();
        }

        private void Execut_LoadActiveProfile()
        {
            View.LoadProfileDialog newDialog = new View.LoadProfileDialog();
            LoadProfileDiagViewModel diagVM = new LoadProfileDiagViewModel(newDialog);
            newDialog.DataContext = diagVM;

            if (newDialog.ShowDialog() == true && diagVM.ProfilList_SelectedIndex >= 0)
            {
                this.ActiveProfile = Model.Profile.LoadProfileFromFile(diagVM.ProfileList[diagVM.ProfilList_SelectedIndex]);
            }
        }

        private void Execute_ExportCardPack()
        {
            View.LoadCardPackDialog newDialog = new View.LoadCardPackDialog();
            LoadCardPackDiagViewModel diagVM = new LoadCardPackDiagViewModel(newDialog);
            newDialog.DataContext = diagVM;

            String SelectedCardPack;
            if (newDialog.ShowDialog() == true)
            {
                SelectedCardPack = diagVM.GetSelectedItem();

                System.Windows.Forms.SaveFileDialog saveDialog = new System.Windows.Forms.SaveFileDialog();
                saveDialog.Filter = "Card Pack Zip(*.lcpz)|*.lcpz|Any File (*.*)|*.*";
                saveDialog.Title = "export card pack";
                if (saveDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    Model.CardPack.ExportCardPack(SelectedCardPack, saveDialog.FileName);
                }
            }
        }
        
        private void Execute_ImportCardPack()
        {
            System.Windows.Forms.OpenFileDialog loadDialog = new System.Windows.Forms.OpenFileDialog();
            loadDialog.Filter = "Card Pack Zip (*.lcpz)|*.lcpz|Any File (*.*)|*.*";
            loadDialog.Title = "Import card pack zip";
            if (loadDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                Model.CardPack.ImportCardPack(loadDialog.FileName);
            }
        }

    }
}
