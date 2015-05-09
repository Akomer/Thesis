using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearningCard.View
{
    /// <summary>
    /// Interaction logic for CreateQuest.xaml
    /// </summary>
    public partial class CreateQuest : UserControl
    {
        public CreateQuest()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.DataContext != null)
            {
                // ViewModel.CreateQnAViewModel DC = (ViewModel.CreateQnAViewModel)this.DataContext;
                // DC.QuestionType_SelectionChanged();
            }
        }
    }
}
