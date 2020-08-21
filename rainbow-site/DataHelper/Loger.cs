using System;
using System.Collections.Generic;

namespace rainbow_site
{
    /// <summary>
    /// 日志记录类,包括查看,增加,删除日志方法
    /// </summary>
    class Loger
    {
        static private string currentLog;
        static private List<string> logQueue = new List<string>();
        /// <summary>
        /// 检查日志文件名,使之更新到当天日期
        /// </summary>
        static public void CheckLogFileName()
        {
            currentLog = "log/" + DateTime.Now.ToShortDateString().Replace('/', '-') + ".txt";
            if(!FileHelper.FileExists(currentLog))
                FileHelper.CreatFile(currentLog);
        }
        /// <summary>
        /// 一般日志记录
        /// </summary>
        /// <param name="info"></param>
        static public void Log(string info)
        {

            string infotext = DateTime.Now.ToLocalTime() + " [info] " + info;
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(infotext);
            WriteLogs(infotext);
        }
        /// <summary>
        /// 日志记录方法
        /// </summary>
        /// <param name="info"></param>
        /// <param name="type"></param>
        static public void Log(string info, LogerType type)
        {
            string infotext = "";
            infotext += DateTime.Now.ToLocalTime();
            switch (type)
            {
                case LogerType.Info:
                    Console.ForegroundColor = ConsoleColor.White;
                    infotext += " [Info] ";
                    break;
                case LogerType.Success:
                    Console.ForegroundColor = ConsoleColor.Green;
                    infotext += " [Success] ";
                    break;
                case LogerType.Waring:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    infotext += " [Warning] ";
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    infotext += " [Wrong] ";
                    break;
            }
            infotext += info;
            Console.WriteLine(infotext);
        }
        static public void LogWrong(string Wrong){
            string infotext = DateTime.Now.ToLocalTime() + " [wrong] " + Wrong;
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(infotext);
            WriteLogs(infotext);
        }
        /// <summary>
        /// 检查队列长度,达到50即写入日志文件
        /// </summary>
        /// <param name="infotext"></param>
        static private void CheckQueue(string infotext)
        {
            logQueue.Add(infotext);

            if (logQueue.Count >= 50)
            {
                string queue = "";
                foreach (string item in logQueue)
                {
                    queue += item + Environment.NewLine;
                }
                WriteLogs(queue);
                //写完日志后,清除队列
                logQueue.Clear();
            }
        }
        /// <summary>
        /// 将日志文件写入
        /// </summary>
        /// <param name="logs"></param>
        static private void WriteLogs(string logs)
        {
            CheckLogFileName();
            FileHelper.WriteFile(currentLog, logs, WriteMode.Append);
        }
    }
    public enum LogerType { Info, Success, Waring, Wrong };
}