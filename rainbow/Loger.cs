using System;

namespace rainbow
{
    /// <summary>
    /// 日志记录类,包括查看,增加,删除日志方法
    /// </summary>
    class Loger
    {
        static public readonly string logPath = "log/log.txt";
        static Loger()
        {
            if (!FileHelper.FileExists(logPath))
                FileHelper.CreatFile(logPath);
        }
        static public void Log(string info, LogerType type)
        {
            string foretext;
            switch (type)
            {
                case LogerType.Info:
                    Console.ForegroundColor = ConsoleColor.White;

                    break;
                case LogerType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case LogerType.Waring:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
            }

        }
    }
    public enum LogerType { Info, Success, Waring, Wrong };
}