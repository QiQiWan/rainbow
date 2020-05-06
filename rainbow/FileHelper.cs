using System.IO;

namespace rainbow
{
    public class FileHelper
    {
        private static FileStream fileStream;
        private static StreamReader streamReader;
        private static StreamWriter streamWriter;

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