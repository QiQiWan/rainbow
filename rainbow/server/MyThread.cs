using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Timers;

namespace rainbow
{
    /// <summary>
    /// 多线程服务器
    /// </summary>
    abstract class myThread
    {
        Thread thread = null;
        System.Timers.Timer timer = new System.Timers.Timer();
        public myThread()
        {
            timer.Elapsed += new ElapsedEventHandler(AutoShop);
            timer.Interval = 30000;
        }

        private void AutoShop(object sender, ElapsedEventArgs args)
        {
            Abort();
            timer.Dispose();
        }

        abstract public void Run();
        public void Start()
        {
            if (thread == null)
                thread = new Thread(Run);
            thread.Start();
        }
        public void Abort()
        {
            BeforeAbort();
        }
        abstract public void BeforeAbort();
    }
    class WebSocketThread : myThread
    {
        private Socket sc;
        public WebSocketThread(Socket socket)
        {
            sc = socket;
        }
        private byte[] buffer = new byte[1024];
        public override void Run()
        {
            Loger.Log("客户端:" + sc.RemoteEndPoint.ToString() + "已连接", LogerType.Info);

            //握手
            int length = sc.Receive(buffer);//接受客户端握手信息
            sc.Send(SocketPacker.PackHandShakeData(SocketPacker.GetSecKeyAccept(buffer, length)));
            Loger.Log("已经发送握手协议", LogerType.Info);

            //发送数据
            string sendMsg = "";
            sc.Send(SocketPacker.PackData(sendMsg));
            Loger.Log("已发送：“" + sendMsg, LogerType.Info);
            this.Abort();
        }
        public override void BeforeAbort()
        {
            sc.Close();
        }
    }

    class HttpThread : myThread
    {
        private HttpListenerContext result;
        public HttpThread(HttpListenerContext result)
        {
            this.result = result;
        }
        public override void Run()
        {
            RainbowServer.SetResponse(result);
            Abort();
        }
        public override void BeforeAbort() { }
    }
}