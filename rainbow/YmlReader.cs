using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace rainbow
{
    /// <summary>
    /// 资源阅读器公共类,包括获取资源文件夹的文件列表,读取文件,对象处理等等
    /// </summary>
    class YmlReader
    {
        static public DirectoryInfo workapace = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory + "resouse");
        //获取句子文件列表
        static public FileInfo[] GetResouceList() => workapace.GetFiles();
        static public string CreateFullName(FileInfo file) => "resource/" + file.Name;
        static public readonly string[] FileLists = { "movies.yml", "reading.yml", "songs.yml" };

        static public SModelManager InitManager()
        {
            SModelManager manager = new SModelManager();
            List<SModel> list = GetModels();

            manager.AddSModels(list);

            return manager;
        }
        static private List<SModel> GetModels()
        {
            List<SModel> list = new List<SModel>();
            int i = 1000;
            foreach (var path in FileLists)
            {
                string fileName = "resource/" + path;
                string origin = FileHelper.ReadFile(fileName);

                SModelType type;

                switch (path)
                {
                    case "movies.yml":
                        type = SModelType.Movies;
                        break;
                    case "reading.yml":
                        type = SModelType.Reading;
                        break;
                    default:
                        type = SModelType.Songs;
                        break;
                }

                foreach (var item in RegexHelper.MatchObj(RegexHelper.objPattern, origin))
                {
                    list.Add(GetModel(item, i, type));
                    i++;
                }
            }
            return list;
        }
        static private SModel GetModel(string origin, int count, SModelType type)
        {
            List<string> Attributes = new List<string>();

            for (int i = 0, len = SModel.Attributes.Length; i < len; i++)
                Attributes.Add(RegexHelper.MatchEle(RegexHelper.elePattern, SModel.Attributes[i], origin));

            Attributes.Add(count.ToString());

            string[] AttArr = Attributes.ToArray();

            return new SModel(AttArr[0], AttArr[1], AttArr[2], AttArr[3], type);
        }

    }
    class RegexHelper
    {
        public static readonly string objPattern = "\\-\\s\\{[\\S\\s]*?(?=\\})";
        public static readonly string elePattern = "{0}:[\\s]\"[\\S\\s]+?(?=\")";
        public static readonly string routerPattern = "/[(\\w|/|.)]*:[\\s][\\w|.|/]+";
        public static readonly string queryPattern = "{0}=[\\w]+";
        public static List<string> MatchObj(string pattern, string origin)
        {
            List<string> matchList = new List<string>();

            foreach (Match item in Regex.Matches(origin, pattern))
            {
                matchList.Add(item.Value);
            }
            return matchList;
        }
        public static List<string> MatchEles(string pattern, string eleName, string origin)
        {
            List<string> matchList = new List<string>();

            pattern = pattern.Replace("{0}", eleName);

            foreach (Match item in Regex.Matches(origin, pattern))
            {
                string temp = item.Value.Replace(eleName + ": \"", "");
                matchList.Add(temp);
            }
            return matchList;
        }
        public static string MatchEle(string pattern, string eleName, string origin)
        {
            pattern = pattern.Replace("{0}", eleName);

            Match match = Regex.Match(origin, pattern);

            string result = match.Value.Replace(eleName + ": \"", "");

            return result;
        }
        public static bool Test(string pattern, string origin)
        {
            if (Regex.Matches(origin, pattern).Count < 1) return false;
            else return true;
        }
        public static string MatchQueEle(string pattern, string eleName, string origin)
        {
            pattern = pattern.Replace("{0}", eleName);

            string result = Regex.Match(origin, pattern).Value.Replace(eleName + "=", "");

            return result;
        }
    }
}
