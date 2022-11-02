using System;
using System.Collections.Generic;

namespace rainbow_site
{
    public class Common
    {
        public static object Lock = new object();//进程锁
        public static string backgroundImg;
        public static DateTime date = DateTime.Now;
        public static void InitBGImg()
        {
            string[] filePaths = FileHelper.GetFileList("wwwroot/img/mainbackground/");
            backgroundImg = "/img/mainbackground/" + filePaths[new Random().Next(0, filePaths.Length)];
        }
        /// <summary>
        /// 每天更新背景图片
        /// </summary>
        public static void UpdateDate()
        {
            if (date != DateTime.Now)
            {
                date = DateTime.Now;
                InitBGImg();
            }
        }
        public Common()
        {
            InitBGImg();
        }
    }

    public class WeLM
    {   
        public static string url = "https://welm.weixin.qq.com/v1/completions";
        public static string Token = "cchi48mv9mc753cgss10";    
        public static string model = "xl";
        public static string temperature = "0.5";
        public static string top_p = "0.0";
        public static string top_k = "30";
        public static string n = "1";
        public static string echo = "true";
        public static string stop = "，。";
        public static Dictionary<string, string> GetParamPairs(){
            Dictionary<string, string> param = new Dictionary<string, string>();
            param.Add("model", model);
            param.Add("temperature", temperature);
            param.Add("top_p", top_p);
            param.Add("top_k", top_k);
            param.Add("n", n);
            param.Add("echo", echo);
            param.Add("stop", stop);
            return param;
        }
    }
}