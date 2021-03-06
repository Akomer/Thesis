﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Windows.Media.Imaging;
using System.IO;
using System.Runtime.Serialization.Json;

namespace LearningCardClasses
{
    [DataContract]
    public class Profile
    {
        [DataMember]
        public String Name { get; set; }
        [DataMember]
        private Dictionary<String, StatisticInfo> StatisticData { get; set; }
        [DataMember]
        public Uri ImageSource { get; set; }
        [DataMember]
        public String LanguageFile { get; set; }
        public BitmapImage ProfilePicture 
        {
            get
            {
                if (this.ImageSource == null)
                {
                    return new BitmapImage(new Uri(@"/Images/error1.jpg", UriKind.Relative));
                }
                try
                {
                    return new BitmapImage(this.ImageSource);
                }
                catch (FileNotFoundException)
                {
                    return new BitmapImage(new Uri(@"/Images/error1.jpg", UriKind.Relative));
                }
            }
            set
            {
                this.ImageSource = value.UriSource;
            }
        }

        public Profile(String name = "Guest")
        {
            this.Name = name;
            //this.ProfilePicture = new BitmapImage(new Uri(@"\\Images\\question_mark.png", UriKind.Relative));
            this.ImageSource = new Uri(@"/Images/question_mark.png", UriKind.Relative);
            this.StatisticData = new Dictionary<string, StatisticInfo>();
        }

        public List<Int32> GetStatInfo(String DeckFile)
        {
            StatisticInfo o;
            if (StatisticData.TryGetValue(DeckFile, out o))
            {
                if (o.CardPackName == CardPack.LoadCardPackFromFile(DeckFile).PackName)
                {
                    return o.StatInfo;
                }
            }
            return null;
        }

        public void AddNewStatData(CardPack cards)
        {
            List<Int32> l = new List<int>();
            foreach (var i in cards.CardList)
            {
                l.Add(5);
            }
            //this.StatisticData.Add(cards.PackName, l);
        }

        public static void SaveProfileToFile(String fileName, Profile tmpProfile)
        {
            String profilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            String profileDirPath = profilePath += @"\\Profiles\\";
            profilePath = profileDirPath + tmpProfile.Name + @".prof";

            if (!Directory.Exists(profileDirPath))
            {
                Directory.CreateDirectory(profileDirPath);
            }
            using (FileStream fStream = new FileStream(profilePath, FileMode.Create, FileAccess.Write))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Profile));
                serializer.WriteObject(fStream, tmpProfile);
            }
        }

        public static Profile LoadProfileFromFile(String profileName)
        {
            String profilePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            profilePath += @"\\Profiles\\" + profileName + @".prof";
            Profile tmpProfile;
            using (FileStream fStream = new FileStream(profilePath, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Profile));
                tmpProfile = (Profile)serializer.ReadObject(fStream);
            }
            return tmpProfile;
        }

        public static List<String> ListOfAvailableProfiles()
        {
            List<String> profileList = new List<String>();
            String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            path += @"\\Profiles\\";
            foreach (String file in Directory.GetFiles(path))
            {
                profileList.Add(file.Split('\\').Last().Split('.')[0]);
            }
            return profileList;
        }
    }
}
