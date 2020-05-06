
namespace rainbow
{
    /// <summary>
    /// 全局变量
    /// </summary>
    class Common
    {
        public static readonly SModelManager manager;

        static Common()
        {
            manager = YmlReader.InitManager();
        }
        public static void init()
        {
            Loger.Log( "初始化句子!", LogerType.Success);
        }
    }
}