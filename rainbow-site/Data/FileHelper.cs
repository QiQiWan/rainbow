using System.IO;

namespace rainbow_site
{
    public class FileHelper
    {
        public static string[] GetFileList(string dirPath){
            DirectoryInfo dir = new DirectoryInfo(dirPath);
            FileInfo[] files = dir.GetFiles();
            string[] filePaths = new string[files.Length];
            for(int i = 0; i < files.Length; i++){
                filePaths[i] = files[i].Name;
            }
            return filePaths;
        }
    }
}