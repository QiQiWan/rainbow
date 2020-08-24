using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace rainbow
{
    /// <summary>
    /// 定义彩虹API服务器,包括监听和消息处理
    /// </summary>
    public class RainbowServer
    {
        private HttpListener server;
        private List<string> domainList = new List<string>();
        private List<HttpThread> threadPools = new List<HttpThread>();
        public RainbowServer()
        {
            server = new HttpListener();
        }
        public void Start()
        {
            if (domainList.Count < 1)
                domainList.Add(FileHelper.ReadFile("server/host"));
            foreach (var item in domainList)
                server.Prefixes.Add(item);
            //初始化路由列表
            Router.init();
            server.Start();
            //IAsyncResult result = server.BeginGetContext(new AsyncCallback(SetResponse), server);
        }
        /// <summary>
        /// 阻塞进程
        /// </summary>
        public void WaitRequest()
        {
            while (true)
            {
                HttpListenerContext result = server.GetContext();
                HttpThread temp = new HttpThread(result);
                AddThread(temp);
                temp.Start();
                threadPools.Remove(temp);
            }
        }
        /// <summary>
        /// 将请求传递给CGI,并返回响应结果
        /// </summary>
        /// <param name="result"></param>
        static public void SetResponse(HttpListenerContext result)
        {

            HttpListenerResponse response = result.Response;
            //允许跨域请求
            response.Headers.Add("Access-Control-Allow-Origin", "*");
            response.Headers.Add("Access-Control-Allow-Methods", "GET, POST");
            response.Headers.Add("Content-Type", "text/plain");

            string responseString = CGI.GetResponse(result.Request);

            byte[] buffer;

            // 正常响应
            buffer = System.Text.Encoding.UTF8.GetBytes(responseString);

            // 搜索引擎抓取
            if (responseString == "robots")
            {
                string robotFile = "robots.txt";
                FileInfo file = new FileInfo(robotFile);//创建一个文件对象
                response.ContentEncoding = Encoding.Default;//输出内容的编码为默认编码
                //response.AddHeader("Content-Disposition", "attachment;filename=" + file.Name);//添加头信息。为“文件下载/另存为”指定默认文件名称
                response.AddHeader("Content-Disposition", "inline");

                FileStream fs = new FileStream(robotFile, FileMode.Open, FileAccess.Read, FileShare.Read);
                buffer = new byte[fs.Length];
                fs.Read(buffer, 0, buffer.Length);
                fs.Close();
            }
            // 格式化句子
            if(responseString == "format"){
                string formatFile = "format.html";
                buffer = FileHelper.ReadBuffer(formatFile);

                // 防止作为附件下载
                response.AddHeader("Content-Type", "text/html; charset=UTF-8");
                response.AddHeader("Content-Disposition", "inline");
            }

            response.ContentLength64 = buffer.Length;
            System.IO.Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
            response.Close();

            Loger.Log("客户端请求地址：" + result.Request.Url.AbsolutePath);
        }

        /// <summary>
        /// 停止服务器
        /// </summary>
        public void Stop()
        {
            server.Stop();
            server.Close();
            server.Abort();
        }
        public void AddDomain(string domain) => domainList.Add(domain);
        private void AddThread(HttpThread thread)
        {
            this.threadPools.Add(thread);
        }
    }
    /// <summary>
    /// 网络通信信息流打包器
    /// </summary>
    class SocketPacker
    {
        static string byte_to_string(byte[] b)
        {
            string s = "";
            foreach (byte _b in b)
            {
                s += _b.ToString();
            }
            return s;
        }
        /// <summary>
        /// 打包握手信息
        /// </summary>
        /// <param name="secKeyAccept">Sec-WebSocket-Accept</param>
        /// <returns>数据包</returns>
        public static byte[] PackHandShakeData(string secKeyAccept)
        {
            var responseBuilder = new StringBuilder();
            responseBuilder.Append("HTTP/1.1 101 Switching Protocols" + Environment.NewLine);
            responseBuilder.Append("Upgrade: websocket" + Environment.NewLine);
            responseBuilder.Append("Connection: Upgrade" + Environment.NewLine);
            //responseBuilder.Append("Sec-WebSocket-Accept: " + secKeyAccept + Environment.NewLine + Environment.NewLine);
            //如果把上一行换成下面两行，才是thewebsocketprotocol-17协议，但居然握手不成功，目前仍没弄明白！
            responseBuilder.Append("Sec-WebSocket-Accept: " + secKeyAccept + Environment.NewLine);
            responseBuilder.Append("Sec-WebSocket-Protocol: chat");

            return Encoding.UTF8.GetBytes(responseBuilder.ToString());
        }

        /// <summary>
        /// 生成Sec-WebSocket-Accept
        /// </summary>
        /// <param name="handShakeText">客户端握手信息</param>
        /// <returns>Sec-WebSocket-Accept</returns>
        public static string GetSecKeyAccept(byte[] handShakeBytes, int bytesLength)
        {
            string handShakeText = Encoding.UTF8.GetString(handShakeBytes, 0, bytesLength);
            string key = string.Empty;
            Regex r = new Regex(@"Sec\-WebSocket\-Key:(.*?)\r\n");
            Match m = r.Match(handShakeText);
            if (m.Groups.Count != 0)
            {
                key = Regex.Replace(m.Value, @"Sec\-WebSocket\-Key:(.*?)\r\n", "$1").Trim();
            }
            byte[] encryptionString = SHA1.Create().ComputeHash(Encoding.ASCII.GetBytes(key + "258EAFA5-E914-47DA-95CA-C5AB0DC85B11"));
            return Convert.ToBase64String(encryptionString);
        }

        /// <summary>
        /// 解析客户端数据包
        /// </summary>
        /// <param name="recBytes">服务器接收的数据包</param>
        /// <param name="recByteLength">有效数据长度</param>
        /// <returns></returns>
        public static string AnalyticData(byte[] recBytes, int recByteLength)
        {
            if (recByteLength < 2) { return string.Empty; }

            bool fin = (recBytes[0] & 0x80) == 0x80; // 1bit，1表示最后一帧  
            if (!fin)
            {
                return string.Empty;// 超过一帧暂不处理 
            }

            bool mask_flag = (recBytes[1] & 0x80) == 0x80; // 是否包含掩码  
            if (!mask_flag)
            {
                return string.Empty;// 不包含掩码的暂不处理
            }

            int payload_len = recBytes[1] & 0x7F; // 数据长度  

            byte[] masks = new byte[4];
            byte[] payload_data;

            if (payload_len == 126)
            {
                Array.Copy(recBytes, 4, masks, 0, 4);
                payload_len = (UInt16)(recBytes[2] << 8 | recBytes[3]);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 8, payload_data, 0, payload_len);

            }
            else if (payload_len == 127)
            {
                Array.Copy(recBytes, 10, masks, 0, 4);
                byte[] uInt64Bytes = new byte[8];
                for (int i = 0; i < 8; i++)
                {
                    uInt64Bytes[i] = recBytes[9 - i];
                }
                UInt64 len = BitConverter.ToUInt64(uInt64Bytes, 0);

                payload_data = new byte[len];
                for (UInt64 i = 0; i < len; i++)
                {
                    payload_data[i] = recBytes[i + 14];
                }
            }
            else
            {
                Array.Copy(recBytes, 2, masks, 0, 4);
                payload_data = new byte[payload_len];
                Array.Copy(recBytes, 6, payload_data, 0, payload_len);

            }

            for (var i = 0; i < payload_len; i++)
            {
                payload_data[i] = (byte)(payload_data[i] ^ masks[i % 4]);
            }

            return Encoding.UTF8.GetString(payload_data);
        }


        /// <summary>
        /// 打包服务器数据
        /// </summary>
        /// <param name="message">数据</param>
        /// <returns>数据包</returns>
        public static byte[] PackData(string message)
        {
            byte[] contentBytes = null;
            byte[] temp = Encoding.UTF8.GetBytes(message);

            if (temp.Length < 126)
            {
                contentBytes = new byte[temp.Length + 2];
                contentBytes[0] = 0x81;
                contentBytes[1] = (byte)temp.Length;
                Array.Copy(temp, 0, contentBytes, 2, temp.Length);
            }
            else if (temp.Length < 0xFFFF)
            {
                contentBytes = new byte[temp.Length + 4];
                contentBytes[0] = 0x81;
                contentBytes[1] = 126;
                contentBytes[2] = (byte)(temp.Length & 0xFF);
                contentBytes[3] = (byte)(temp.Length >> 8 & 0xFF);
                Array.Copy(temp, 0, contentBytes, 4, temp.Length);
            }
            else
            {
                // 暂不处理超长内容  
            }

            return contentBytes;
        }
    }
}