using System.IO;

namespace rainbow
{

    /// <summary>
    /// 文件帮助类，负责读写创建文件
    /// </summary>
    public class FileHelper
    {
        private static FileStream fileStream;
        private static StreamReader streamReader;
        private static StreamWriter streamWriter;
        /// <summary>
        /// 读取指定文本文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static string ReadFile(string filePath)
        {
            string fullText = "";

            if (!File.Exists(filePath))
            {
                Loger.Log(filePath + "  File don't exist!!!", LogerType.Wrong);
            }

            using (fileStream = new FileStream(filePath, FileMode.Open))
            {
                streamReader = new StreamReader(fileStream);
                fullText = streamReader.ReadToEnd();
                streamReader.Close();
                fileStream.Close();
            }
            return fullText;
        }
        /// <summary>
        /// 将指定内容按照指定方式写入指定文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <param name="mode"></param>
        public static void WriteFile(string filePath, string content, WriteMode mode)
        {
            FileMode fileMode = mode == WriteMode.WriteAll ? FileMode.OpenOrCreate : FileMode.Append;
            if (!File.Exists(filePath))
                File.Create(filePath);
            using (fileStream = new FileStream(filePath, fileMode))
            {
                streamWriter = new StreamWriter(fileStream);
                streamWriter.Write(content);
                streamWriter.Close();
                fileStream.Close();
            }
        }
        public static bool FileExists(string filePath) => File.Exists(filePath);
        public static void CreatFile(string filePath) => File.CreateText(filePath);
    }
    public enum WriteMode { Append, WriteAll };
}