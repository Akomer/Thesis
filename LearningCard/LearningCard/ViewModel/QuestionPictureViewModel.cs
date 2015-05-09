using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Forms;

namespace LearningCard.ViewModel
{
    class QuestionPictureViewModel : ViewModelBase
    {
        public BitmapImage ImageSource { get; set; }
        public Boolean EnableImageChange { get; set; }
        public DelegateCommand Command_ChangeImage { get; set; }

        public QuestionPictureViewModel()
        {
            this.EnableImageChange = false;

            this.ImageSource = new BitmapImage();
            this.ImageSource.BeginInit();
            this.ImageSource.UriSource = new Uri("C:\\Users\\Speeder\\Pictures\\kirito1.jpg");
            this.ImageSource.EndInit();

            this.Command_ChangeImage = new DelegateCommand(x => this.Execute_ChangeImage());
        }

        private void Execute_ChangeImage()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Images (*.jpg, *jpeg, *.png, *.bmp)|*.jpg; *jpeg; *.png; *.bmp" ;
            dialog.Title = "Select an image file";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                this.ImageSource = new BitmapImage();
                this.ImageSource.BeginInit();
                this.ImageSource.UriSource = new Uri(dialog.FileName);
                this.ImageSource.EndInit();

                this.OnPropertyChanged("ImageSource");
            }
        }
    }
}
