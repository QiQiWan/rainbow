
namespace rainbow
{
    /// <summary>
    /// 全局变量
    /// </summary>
    class Common
    {
        //进程锁
        public static object Lock = new object();
        public static SModelManager manager;
        public static RainbowServer server;

        static Common()
        {
            manager = YmlReader.InitManager();
            Loger.Log("句子文件列表更新完成!");

            InitServer();
            Loger.Log("服务器初始化完成!");
        }
        public static void InitServer()
        {
            server = new RainbowServer();
        }
        public static void InitManager(){
            manager = YmlReader.InitManager();
        }
        public static void init()
        {
        }
    }
}