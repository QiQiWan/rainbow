using System;
using System.IO;

namespace rainbow
{
    /// <summary>
    /// 资源阅读器公共类,包括获取资源文件夹的文件列表,读取文件,对象处理等等
    /// </summary>
    class YmlReader{
        static public DirectoryInfo workapace = new DirectoryInfo( AppDomain.CurrentDomain.BaseDirectory + "resouse");
        //获取句子文件列表
        static public FileInfo[] GetResouceList() => workapace.GetFiles();
        static public string CreateFullName(FileInfo file) => "resource/" + file.Name;





    }
}
