using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Forms;
using System.IO;

namespace LearningCard.ViewModel
{
    class QuestionPictureViewModel : QuestionViewModelBase
    {
        private Model.QuestionPictureModel QuestionModel;

        public BitmapImage ImageSource
        {
            get
            {
                try
                {
                    return new BitmapImage(this.QuestionModel.GetImageFullPath());
                }
                catch (Exception)
                {
                    return new BitmapImage(new Uri(@"/Images/error1.jpg", UriKind.Relative));
                }
            }
            set { }
        }
        public Boolean EnableImageChange { get; set; }
        public DelegateCommand Command_ChangeImage { get; set; }
        public String QuestionText
        {
            get
            {
                return this.QuestionModel.Text;
            }
            set 
            {
                this.QuestionModel.Text = value;
            }
        }

        public QuestionPictureViewModel(Model.QuestionPictureModel qModel, Boolean changeAble = true)
            : base(qModel, changeAble)
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
                this.QuestionModel.ImageSRC = new Uri(dialog.FileName);
                this.OnPropertyChanged("ImageSource");
            }
        }
    }
}
