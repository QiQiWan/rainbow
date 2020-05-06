using System;

namespace rainbow
{
    /// <summary>
    /// 句子模型类,包括句子的格式化,和储存操作等
    /// </summary>
    class SModel{
        /// <summary>
        /// 储存句子的内容,作者和来源
        /// </summary>
        public readonly string content;
        public readonly string author;
        public readonly string source;
        /// <summary>
        /// 使用new以初始化句子模型
        /// </summary>
        /// <param name="content">内容</param>
        /// <param name="author">作者</param>
        /// <param name="source">来源</param>
        public SModel(string content, string author, string source){
            this.content = content;
            this.author = author;
            this.source = source;
        }
        public override string ToString(){
            string text = "";
            text += String.Format("Content: {0}", content) + Environment.NewLine;
            text += String.Format("Author: {0}", author) + Environment.NewLine;
            text += String.Format("Source: {0}", source) + Environment.NewLine;
            return text;
        }
    }
}