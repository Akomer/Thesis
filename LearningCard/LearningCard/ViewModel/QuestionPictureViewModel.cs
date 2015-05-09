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
        private Model.QuestionPicture QuestionModel;

        public BitmapImage ImageSource
        {
            get
            {
                BitmapImage img_src = new BitmapImage();
                img_src.BeginInit();
                // img_src.UriSource = new Uri("C:\\Users\\Speeder\\Pictures\\kirito1.jpg");
                img_src.UriSource = (Uri)this.QuestionModel.GetQuestion()[0].Data;
                img_src.EndInit();
                return img_src;
            }
            set { }
        }
        public Boolean EnableImageChange { get; set; }
        public DelegateCommand Command_ChangeImage { get; set; }
        public String QuestionText
        {
            get
            {
                return (String)this.QuestionModel.GetQuestion()[1].Data;
            }
            set 
            {
                // this.QuestionModel.
            }
        }

        public QuestionPictureViewModel(Model.QuestionPicture qModel)
        {
            this.QuestionModel = qModel;
            this.EnableImageChange = false;

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
