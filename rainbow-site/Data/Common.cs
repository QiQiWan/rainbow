using System;

namespace rainbow_site
{
    public class Common
    {
        public static string backgroundImg;
        public static DateTime date = DateTime.Now;

        public static void InitBGImg(){
            string[] filePaths = FileHelper.GetFileList("wwwroot/img/");
            backgroundImg = "/img/" + filePaths[new Random().Next(0, filePaths.Length)];
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