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
    class GlobalLanguage
    {
        private static GlobalLanguage instance;
        private Dictionary<String, String> langDict;
        private List<String> langList;
        private static String ActiveFile;

        private GlobalLanguage(String fname) 
        {
            langDict = new Dictionary<string, string>();
            ActiveFile = fname;
            ReadJson();
        }

        public static GlobalLanguage Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GlobalLanguage("eng");
                }
                return instance;
            }
        }

        public void SetLanguage(String lang)
        {
            ActiveFile = lang;
            ReadJson();
        }

        public String GetLanguageFile()
        {
            return ActiveFile;
        }

        private void ReadJson()
        {
            String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
            //path += "\\Language\\";
            path += @"/Language/" + ActiveFile + @".lng";
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
                o = langDict["NotFindInDict"] + ActiveFile + ".lng";
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
            Regex reg = new Regex("\"([^\"]+)\"\\s*:\\s*\"([^\"]+)\",");
            MatchCollection m = reg.Matches(a);
            foreach (Match item in m)
            {
                d.Add(item.Groups[1].ToString(), item.Groups[2].ToString());
            }
            return d;
        }

        private String FindLanguageName(String fileName)
        {
            return this.reParseJson(System.IO.File.ReadAllText(fileName))["LanguageName"];
        }

        public List<String> LanguageList()
        {
            if (this.langList == null)
            {
                this.langList = new List<string>();
                String path = Directory.GetParent(System.IO.Directory.GetCurrentDirectory()).Parent.FullName;
                path += "\\Language\\";
                foreach (String file in Directory.GetFiles(path))
                {
                    this.langList.Add(this.FindLanguageName(file));                  
                }
            }
            return this.langList;
        }
    }
}
