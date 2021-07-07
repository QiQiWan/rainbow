using System;

namespace rainbow_site
{
    public class Common
    {
        public static object Lock = new object();//进程锁
        public static string backgroundImg;
        public static DateTime date = DateTime.Now;
        public static void InitBGImg(){
            string[] filePaths = FileHelper.GetFileList("wwwroot/img/mainbackground/");
            backgroundImg = "/img/mainbackground/" + filePaths[new Random().Next(0, filePaths.Length)];
        }
        /// <summary>
        /// 每天更新背景图片
        /// </summary>
        public static void UpdateDate(){
            if(date != DateTime.Now){
                date = DateTime.Now;
                InitBGImg();
            }
        }
        public  Common()
        {
            InitBGImg();
        }
    }
}