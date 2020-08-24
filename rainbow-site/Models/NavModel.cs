namespace rainbow_site{
    public class NavModel{
        public static CultureLanguage Culture = CultureLanguage.CH;
        public static string IndexUrl;
        public static string AboutUrl;
        public static string AboutTitle;
        public NavModel(){
            Refresh();
        }
        public static void Refresh(){
            if(Culture == CultureLanguage.CH){
                IndexUrl = "/";
                AboutTitle = "关于";
                AboutUrl = "/About";
            }
            if(Culture == CultureLanguage.EN){
                IndexUrl = "Index/en";
                AboutTitle = "About";
                AboutUrl = "/About/en";
            }
        }
    }
    public enum CultureLanguage{CH, EN};
}