using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.IO;
using System.Runtime.Serialization.Json;
using System.IO.Compression;

namespace LearningCardClasses
{
    [DataContract]
    public class CardPack
    {
        [DataMember]
        public String PackName { get; set; }
        [DataMember]
        public List<Card> CardList { get; set; }

        static public List<Tuple<CardPack, String>> CardPackList()
        {
            List<Tuple<CardPack, String>> deckList = new List<Tuple<CardPack, String>>();
            String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            path += "\\CardPacks\\";
            foreach (String file in Directory.GetFiles(path, "*.lcp"))
            {
                String deckName = Path.GetFileNameWithoutExtension(file);
                try
                {
                    deckList.Add(new Tuple<CardPack, String>(LoadCardPackFromFile(deckName), Path.GetFileName(file)));
                }
                catch (SerializationException)
                {
                    continue;
                }
            }
            return deckList;
        }

        static public CardPack LoadCardPackFromFile(String deckName)
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
                path += "\\CardPacks\\" + deckName;
                if (!deckName.EndsWith(".lcp"))
                    path += ".lcp";
            }
            using (FileStream fStream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
                {
                    tmpDeck = (CardPack)serializer.ReadObject(fStream);
                }
            }
            return tmpDeck;
        }

        static public void SaveCardPackToFile(CardPack deck)
        {
            String BasePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;

            HashSet<String> localDeckNames = new HashSet<string>();
            String DeckPath = BasePath + "\\CardPacks";
            foreach (String file in Directory.GetFiles(DeckPath, "*.lcp"))
            {
                localDeckNames.Add(Path.GetFileNameWithoutExtension(file));
            }
            String newCardPackName = deck.PackName;
            Int32 i = 0;
            while (localDeckNames.Contains(newCardPackName))
            {
                newCardPackName = deck.PackName + String.Format("_{0}", i);
                i += 1;
            }

            String ImagePath = BasePath + "\\CardPacks\\" + newCardPackName + "_IMG\\";
            String FilePath = BasePath + "\\CardPacks\\" + newCardPackName + ".lcp"; ;

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
                        i = 0;
                        while (imgSet.Contains(imgName))
                        {
                            imgName = BaseImgName + String.Format("_{0}.{1}", i, ImgExtension);
                            i += 1;
                        }
                        System.IO.File.Copy(q.ImageSRC.AbsolutePath, ImagePath + imgName, true);
                        imgSet.Add(imgName);
                        q.ImageSRC = new Uri("\\CardPacks\\" + newCardPackName + "_IMG\\" + imgName, UriKind.Relative);
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

            using (FileStream saveFile = new FileStream(FilePath, FileMode.Create, FileAccess.Write))
            {
                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(CardPack));
                serializer.WriteObject(saveFile, deck);
            }
        }

        static public void ExportCardPack(String PackName, String destFile)
        {
            String BasePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            BasePath += "\\CardPacks";
            try
            {
                File.Copy(BasePath + String.Format("\\{0}.lcp", PackName),
                          BasePath + String.Format("\\{0}_IMG\\{0}.lcp", PackName));
            }
            catch (IOException)
            {
                File.Delete(BasePath + String.Format("\\{0}_IMG\\{0}.lcp", PackName));
                File.Copy(BasePath + String.Format("\\{0}.lcp", PackName),
                    BasePath + String.Format("\\{0}_IMG\\{0}.lcp", PackName));
            }

            ZipFile.CreateFromDirectory(BasePath + String.Format("\\{0}_IMG", PackName), destFile);

            File.Delete(BasePath + String.Format("\\{0}_IMG\\{0}.lcp", PackName));
        }

        static public void ImportCardPack(String sourceFile)
        {
            String BasePath = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            String importDir = BasePath + "\\tmp\\import";
            Directory.CreateDirectory(importDir);
            try
            {
                ZipFile.ExtractToDirectory(sourceFile, importDir);
            }
            catch (IOException)
            {
                Directory.Delete(importDir, true);
                ZipFile.ExtractToDirectory(sourceFile, importDir);
            }

            String cardPack = Directory.GetFiles(importDir, "*.lcp")[0];
            String cardPackName = Path.GetFileNameWithoutExtension(cardPack);

            String DeckPath = BasePath + "\\CardPacks";
            HashSet<String> localDeckNames = new HashSet<string>();

            foreach (String file in Directory.GetFiles(DeckPath))
            {
                localDeckNames.Add(Path.GetFileNameWithoutExtension(file));
            }
            String newCardPackName = cardPackName;
            Int32 i = 0;
            while (localDeckNames.Contains(newCardPackName))
            {
                newCardPackName = cardPackName + String.Format("_{0}", i);
                i += 1;
            }

            String newImportedDeckPath = DeckPath + "\\" + newCardPackName + "_IMG";
            try
            {
                Directory.Move(importDir, newImportedDeckPath);
            }
            catch
            {
                Directory.Delete(newImportedDeckPath, true);
                Directory.Move(importDir, newImportedDeckPath);
            }
            String a = newImportedDeckPath + String.Format("\\{0}.lcp", cardPackName);
            String b = DeckPath + String.Format("\\{0}.lcp", newCardPackName);
            File.Move(a, b);

        }
    }
}
