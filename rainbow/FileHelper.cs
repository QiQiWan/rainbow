using System.Collections.Generic;
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
                CreatFile(filePath);
            using (fileStream = new FileStream(filePath, FileMode.Open))
            {
                streamReader = new StreamReader(fileStream);
                fullText = streamReader.ReadToEnd();
                streamReader.Close();
                fileStream.Close();
            }
            return fullText;
        }
        public static string[] ReadFileLine(string filePath)
        {
            List<string> fileLines = new List<string>();
            using (fileStream = new FileStream(filePath, FileMode.Open))
            {
                streamReader = new StreamReader(fileStream);
                string line;
                while ((line = streamReader.ReadLine()) != null)
                {
                    fileLines.Add(line);
                }
                streamReader.Close();
                fileStream.Close();
            }
            return fileLines.ToArray();
        }
        /// <summary>
        /// 将文件读取为二进制数组
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        public static byte[] ReadBuffer(string filePath)
        {
            byte[] buffer;
            using (fileStream = new FileStream(filePath, FileMode.Open))
            {
                buffer = new byte[fileStream.Length];
                fileStream.Read(buffer, 0, (int)fileStream.Length);
                fileStream.Close();
            }
            return buffer;
        }
        /// <summary>
        /// 将指定内容按照指定方式写入指定文件
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="content"></param>
        /// <param name="mode"></param>
        public static void WriteFile(string filePath, string content, WriteMode mode)
        {
            if (!File.Exists(filePath))
                CreatFile(filePath);
            lock (Common.Lock)
            {
                using (fileStream = new FileStream(filePath, FileMode.Open))
                {
                    //文件指针定位到文件尾部
                    if (mode == WriteMode.Append)
                        fileStream.Position = fileStream.Length;
                    streamWriter = new StreamWriter(fileStream);
                    streamWriter.WriteLine(content);
                    streamWriter.Close();
                    fileStream.Close();
                }
            }
        }
        public static bool FileExists(string filePath) => File.Exists(filePath);
        public static void CreatFile(string filePath)
        {
            using (fileStream = new FileStream(filePath, FileMode.Create))
            {
                streamWriter = new StreamWriter(fileStream);
                streamWriter.WriteLine("");
                streamWriter.Close();
                fileStream.Close();
            }
        }
    }
    public enum WriteMode { Append, WriteAll };
}