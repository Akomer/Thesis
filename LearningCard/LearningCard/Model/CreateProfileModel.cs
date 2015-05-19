using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.IO;
using System.Runtime.Serialization.Json;
using LearningCardClasses;

namespace LearningCard.Model
{
    class CreateProfileModel
    {
        public String UserName { get; set; }
        public BitmapImage ProfilPicture { get; set; }

        public CreateProfileModel()
        {
            this.ProfilPicture = new BitmapImage(new Uri(@"/Images/question_mark.png", UriKind.Relative));
        }

        public void SaveProfile()
        {
            if (this.UserName == "" || this.UserName == null)
            {
                System.Windows.Forms.MessageBox.Show("Can not save profile without Name", "Missing Name",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                return;
            }
            String profilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            profilePath += @"\\Profiles\\" + this.UserName + @".prof";
            if (File.Exists(profilePath))
            {
                if (System.Windows.Forms.MessageBox.Show("Profile already exsits\nDo you want overwrite it?", "Profile exsits",
                    System.Windows.Forms.MessageBoxButtons.YesNo,
                    System.Windows.Forms.MessageBoxIcon.Exclamation) == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            Profile tmpProfile = new Profile(this.UserName);
            tmpProfile.ProfilePicture = this.ProfilPicture;
            Profile.SaveProfileToFile(this.UserName, tmpProfile);
            System.Windows.Forms.MessageBox.Show("Profile saved", "Profile saved",
                System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Information);
        }

        public void LoadProfile(String profileName)
        {
            Profile tmpProfile = Profile.LoadProfileFromFile(profileName);
            this.UserName = tmpProfile.Name;
            this.ProfilPicture = (BitmapImage)tmpProfile.ProfilePicture;
        }
    }
}
