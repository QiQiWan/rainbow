using System;

namespace rainbow
{
    class Program
    {
        static RainbowServer server;
        /// <summary>
        /// 主程序函数入口
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {


            //初始化全局变量
            Common.init();
            //引用全局服务器
            server = Common.server;
            //添加服务监听地址
            server.AddDomain("http://127.0.0.1:8888/");

            Loger.Log("The server is running at: http:/127.0.0.1:8888/");

            //设置定时器，一个分钟刷新一次句子管理器
            TimeTick tick = new TimeTick(60000); //单位毫秒
            Handle handle = new Handle(RefreshCatch);
            tick.Start(handle);

            //启动服务器,监控服务状态
            try
            {
                server.Start();
                server.WaitRequest();
            }
            catch (Exception err)
            {
                Loger.LogWrong(err.Message);
            }
            finally
            {
                server.Stop();
            }
        }
        static void RefreshCatch(object source, System.Timers.ElapsedEventArgs e) => Common.InitManager();

    }
}
