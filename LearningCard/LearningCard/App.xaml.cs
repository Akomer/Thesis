using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

using LearningCard;

namespace LearningCard
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private Window mainWindow;
        private ViewModel.MainWindowViewModel mainWindowViewModel;



        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            this.mainWindow = new View.MainWindow();
            this.mainWindowViewModel = new ViewModel.MainWindowViewModel();
            this.mainWindow.DataContext = this.mainWindowViewModel;

            this.mainWindowViewModel.ChangeMainWindowContent += new ViewModel.MyEventDelegate(VM_ChangeMainWindow);

            this.mainWindow.Show();
        }

        private void VM_ChangeMainWindow(ViewModel.ViewModelChangeEventArgs _args)
        {
            this.mainWindow = (Window)Activator.CreateInstance(_args.Data);
        }
    }
}
