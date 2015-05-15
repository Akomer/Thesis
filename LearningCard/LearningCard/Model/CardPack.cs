using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;

namespace LearningCard.Model
{
    [DataContract]
    class CardPack
    {
        [DataMember]
        public String PackName { get; set; }
        [DataMember]
        public List<Card> CardList { get; set; }

        static List<CardPack> CardPackList()
        {
            List<CardPack> deckList = new List<CardPack>();
            deckList = new List<CardPack>();
            String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            path += "\\CardPacks\\";
            foreach (String file in Directory.GetFiles(path, "*.lcp"))
            {

                //deckList.Add();
            }
            return deckList;
        }

        static CardPack LoadCardPackFromFile(String deckName)
        {
            CardPack tmpDeck;
            String path;
            if (deckName.Contains('\\'))
            {
                path = deckName;
            }
            else
            {
                path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                path += "\\CardPacks\\" + deckName + ".lcp";
            }
            using (FileStream fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
                //try
                {
                    tmpDeck = (CardPack)serializer.ReadObject(fStream);
                }
            }
            return tmpDeck;
        }

        static public void SaveCardPackToFile(CardPack deck)
        {
            String BasePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            String ImagePath = BasePath + "\\CardPacks\\" + deck.PackName + "_IMG\\";
            String FilePath = BasePath + "\\CardPacks\\" + deck.PackName + ".lcp"; ;

            if (!System.IO.Directory.Exists(ImagePath))
            {
                System.IO.Directory.CreateDirectory(ImagePath);
            }

            HashSet<String> imgSet = new HashSet<String>();
            foreach (Card item in deck.CardList)
            {
                if (item.Question.GetQuestionType() == typeof(QuestionPictureModel))
                {
                    QuestionPictureModel q = (QuestionPictureModel)item.Question;
                    if (q.ImageSRC.IsAbsoluteUri)
                    {
                        String BaseImgName = System.IO.Path.GetFileNameWithoutExtension(q.ImageSRC.ToString());
                        String ImgExtension = System.IO.Path.GetExtension(q.ImageSRC.ToString());
                        String imgName = BaseImgName + ImgExtension;
                        while (imgSet.Contains(imgName))
                        {
                            Int32 i = 0;
                            imgName = BaseImgName + String.Format("_{0}.{1}", i, ImgExtension);
                            i += 1;
                        }
                        System.IO.File.Copy(q.ImageSRC.AbsolutePath, ImagePath + imgName, true);
                        imgSet.Add(imgName);
                        q.ImageSRC = new Uri("\\CardPacks\\" + deck.PackName + "_IMG\\" + imgName, UriKind.Relative);
                    }
                    else
                    {
                        imgSet.Add(System.IO.Path.GetFileName(q.ImageSRC.ToString()));
                    }
                }
            }

            foreach (String item in System.IO.Directory.GetFiles(ImagePath))
            {
                if (!imgSet.Contains(System.IO.Path.GetFileName(item)))
                {
                    try
                    {
                        System.IO.File.Delete(item);
                    }
                    catch (IOException)
                    {
                        continue;
                    }
                }
            }

            using (FileStream saveFile = new FileStream(FilePath, FileMode.OpenOrCreate, FileAccess.Write))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
                serializer.WriteObject(saveFile, deck);
            }
        }
    }
}
