using System;
using System.Collections.Generic;

namespace rainbow
{

    /// <summary>
    /// 句子模型类,包括句子的格式化,和储存操作等
    /// </summary>
    class SModel
    {
        /// <summary>
        /// 储存句子的内容,作者和来源
        /// </summary>
        public readonly string content;
        public readonly string author;
        public readonly string source;
        public readonly string ID;
        public readonly SModelType type;
        /// <summary>
        /// 需要从文件中读取的属性列表
        /// </summary>
        /// <value></value>
        public static string[] Attributes = { "content", "author", "source" };
        /// <summary>
        /// 使用new以初始化句子模型
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="author">作者</param>
        /// <param name="source">来源</param>
        /// <param name="ID">唯一ID</param>
        /// <param name="type">来源文件</param>
        public SModel(string content, string author, string source, string ID, SModelType type)
        {
            this.content = content;
            this.author = author;
            this.source = source;
            this.ID = ID;
            this.type = type;
        }
        public override string ToString()
        {
            string text = "";
            text += String.Format("Content: {0},", content) + Environment.NewLine;
            text += String.Format("Author: {0},", author) + Environment.NewLine;
            text += String.Format("Source: {0}", source) + Environment.NewLine;
            text += String.Format("ID: {0}", ID) + Environment.NewLine;
            return text;
        }
        public string ToJsonString()
        {
            string json = "{" + Environment.NewLine;
            json += String.Format("\"Content\": \"{0}\",", content) + Environment.NewLine;
            json += String.Format("\"Author\": \"{0}\",", author) + Environment.NewLine;
            json += String.Format("\"Source\": \"{0}\",", source) + Environment.NewLine;
            json += String.Format("\"ID\": \"{0}\"", ID) + Environment.NewLine;
            json += "}";
            return json;
        }
        public string ToTypedString()
        {
            string typed = "";
            typed += String.Format("{0} <br /> ————{1}《{2}》", content, author, source);
            return typed;
        }
    }

    /// <summary>
    /// 句子管理器类，负责将整合句子和抽选句子
    /// </summary>
    class SModelManager
    {
        private List<SModel> reading = new List<SModel>();
        private List<SModel> movies = new List<SModel>();
        private List<SModel> songs = new List<SModel>();
        /// <summary>
        /// 出一个集合数组，便于抽取数据
        /// </summary>
        private SModel[] total;
        public SModelManager()
        {
            init();
        }
        public SModelManager(List<SModel> list)
        {
            init();
            AddSModels(list);
        }
        public void init()
        {
            reading.Clear();
            movies.Clear();
            songs.Clear();
        }
        /// <summary>
        /// 添加句子
        /// </summary>
        /// <param name="type">句子类型</param>
        /// <param name="model"></param>
        private void AddSModel(SModel model)
        {
            switch (model.type)
            {
                case SModelType.Reading:
                    reading.Add(model);
                    break;
                case SModelType.Movies:
                    movies.Add(model);
                    break;
                default:
                    songs.Add(model);
                    break;
            }
        }
        public void AddSModels(List<SModel> list)
        {
            List<SModel> temp = new List<SModel>(); ;
            foreach (var item in list)
            {
                AddSModel(item);
                temp.Add(item);
            }
            total = temp.ToArray();
            GetAllModel();
        }
        /// <summary>
        /// 获取句子总数量
        /// </summary>
        /// <returns></returns>
        public int GetCount() => total.Length;
        /// <summary>
        /// 获取单类型句子数量
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public int GetCount(SModelType type)
        {
            switch (type)
            {
                case SModelType.Reading:
                    return reading.Count;
                case SModelType.Movies:
                    return movies.Count;
                default:
                    return songs.Count;
            }
        }

        /// <summary>
        /// 随机获取句子
        /// </summary>
        /// <returns></returns>
        public SModel GetModel()
        {
            int random = new Random().Next(0, total.Length);
            return total[random];
        }
        public SModel GetModel(SModelType type)
        {
            SModel temp;
            do
                temp = GetModel();
            while (temp.type != type);
            return temp;
        }
        public SModel GetModel(string id)
        {
            if (id != null)
            {
                foreach (var item in total)
                {
                    if (item.ID == id)
                        return item;
                }
            }
            return GetModel();
        }
        /// <summary>
        /// 获取所有的句子字符串列表
        /// </summary>
        /// <returns></returns>
        
        public string AllModelString;
        public string GetAllModel()
        {
            string json = "{\"models\": [";
            for (int i = 0, len = GetCount(); i < len; i++)
            {
                if (i < len - 1)
                    json += $"{total[i].ToJsonString()},{Environment.NewLine}";
                else
                    json += $"{total[i].ToJsonString()}";
            }
            json += "]}";
            return AllModelString = json;
        }
    }
    public enum SModelType { Reading, Movies, Songs, Poetries };
}