using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;
using LearningCardClasses;

namespace LearningCard.Model
{
    class GlobalLanguage
    {
        private static GlobalLanguage instance;
        private Dictionary<String, String> langDict;
        private List<String> langList;
        private static String ActiveFile;
        private Dictionary<String, String> fileToLang;

        static private Int32 NumberOfKeysInLanguageDict = 60;

        private GlobalLanguage() 
        {
            langDict = new Dictionary<string, string>();
            LanguageList();
            try
            {
                ActiveFile = this.fileToLang[LanguageList()[0]];
            }
            catch (ArgumentOutOfRangeException)
            {
                if (this.fileToLang.Count() == 0)
                {
                    System.Windows.Forms.MessageBox.Show("Could not find any proper language file", "Missing Language File",
                        System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
                    return;
                }
            }
            ReadJson();
        }

        public static GlobalLanguage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalLanguage();
                }
                return instance;
            }
        }

        public void SetLanguage(String lang)
        {
            ActiveFile = this.fileToLang[lang];
            ReadJson();
        }

        public String GetLanguageFile()
        {
            return ActiveFile;
        }

        private void ReadJson()
        {
            String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            path += "\\Language\\" + ActiveFile;
            //path += @"/Language/" + ActiveFile;
            StreamReader reader = new StreamReader(path);
            String languge = reader.ReadToEnd();
            reader.Close();
            langDict = reParseJson(languge);
        }

        public String Get(String key)
        {
            String o;
            if ( ! langDict.TryGetValue(key, out o))
            {
                o = langDict["NotFindInDict"] + ActiveFile;
            }
            return o;
        }

        public Dictionary<String, String> GetDict()
        {
            return langDict;
        }

        private Dictionary<String, String> reParseJson(String a)
        {
            Dictionary<String, String> d = new Dictionary<string, string>();
            Regex reg = new Regex("\"([^\"]+)\"\\s*:\\s*\"([^\"]+)\",?");
            MatchCollection m = reg.Matches(a);
            foreach (Match item in m)
            {
                try
                {
                    d.Add(item.Groups[1].ToString(), item.Groups[2].ToString());
                }
                catch(ArgumentException)
                {
                    continue;
                }
            }
            return d;
        }

        private String FindLanguageName(String fileName)
        {
            try
            {
                return this.reParseJson(System.IO.File.ReadAllText(fileName))["LanguageName"];
            }
            catch (KeyNotFoundException)
            {
                return "";
            }
        }

        public List<String> LanguageList()
        {
            if (this.langList == null)
            {
                this.fileToLang = new Dictionary<string, string>();
                this.langList = new List<string>();
                String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                path += "\\Language\\";
                foreach (String file in Directory.GetFiles(path))
                {
                    Dictionary<String, String> dLang = this.reParseJson(System.IO.File.ReadAllText(file));
                    if (dLang.Count == GlobalLanguage.NumberOfKeysInLanguageDict)
                    {
                        String lngName;
                        try
                        {
                            lngName = dLang["LanguageName"];
                        }
                        catch (KeyNotFoundException)
                        {
                            lngName = "";
                        }
                        if (lngName != "")
                        {
                            try
                            {
                                this.fileToLang.Add(lngName, Path.GetFileName(file));
                            }
                            catch (ArgumentException)
                            {
                                continue;
                            }
                            this.langList.Add(lngName);
                        }
                    }
                }
            }
            return this.langList;
        }
    }
}
