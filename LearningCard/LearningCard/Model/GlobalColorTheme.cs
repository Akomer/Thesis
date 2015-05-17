using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text.RegularExpressions;


namespace LearningCard.Model
{
    class GlobalColorTheme
    {
        private static GlobalColorTheme instance;
        private Dictionary<String, String> colorDict;
        private List<String> colorList;
        private static String ActiveFile;

        static private Int32 NumberOfKeysInColorDict = 44;

        private GlobalColorTheme(String fname) 
        {
            colorDict = new Dictionary<string, string>();
            ActiveFile = fname;
            ReadJson();
        }

        public static GlobalColorTheme Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalColorTheme("light");
                }
                return instance;
            }
        }

        public void SetColor(String lang)
        {
            ReadJson();
        }

        public String GetColorFile()
        {
            return ActiveFile;
        }

        private void ReadJson()
        {
            String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            path += "\\ColorThemes\\" + ActiveFile;
            StreamReader reader = new StreamReader(path);
            String languge = reader.ReadToEnd();
            reader.Close();
            colorDict = reParseJson(languge);
        }

        public String Get(String key)
        {
            String o;
            if ( ! colorDict.TryGetValue(key, out o))
            {
                o = colorDict["NotFindInDict"] + ActiveFile;
            }
            return o;
        }

        public Dictionary<String, String> GetDict()
        {
            return colorDict;
        }

        private Dictionary<String, String> reParseJson(String a)
        {
            Dictionary<String, String> d = new Dictionary<string, string>();
            Regex reg = new Regex("\"([^\"]+)\"\\s*:\\s*\"([^\"]+)\",?");
            MatchCollection m = reg.Matches(a);
            foreach (Match item in m)
            {
                d.Add(item.Groups[1].ToString(), item.Groups[2].ToString());
            }
            return d;
        }

        public List<String> ColorList()
        {
            if (this.colorList == null)
            {
                this.colorList = new List<string>();
                String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                path += "\\ColorThemes\\";
                foreach (String file in Directory.GetFiles(path))
                {
                    Dictionary<String, String> dLang = this.reParseJson(System.IO.File.ReadAllText(file));
                    if (dLang.Count == GlobalColorTheme.NumberOfKeysInColorDict)
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
                            {
                                //this.fileToLang.Add(lngName, Path.GetFileName(file));
                            }
                            this.colorList.Add(lngName);
                        }
                    }
                }
            }
            return this.colorList;
        }
    }
}
