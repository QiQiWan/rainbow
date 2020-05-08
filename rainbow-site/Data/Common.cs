using System;

namespace rainbow_site
{
    public class Common
    {
        public static string backgroundImg;
        public static DateTime date = DateTime.Now;

        public static void InitBGImg(){
            backgroundImg = "/img/" + new Random().Next(1, 16) + ".jpg";
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