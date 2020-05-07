using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace rainbow
{
    public class CGI
    {
        public static string GetResponse(HttpListenerRequest request)
        {

            Loger.Log("客户端: " + request.RemoteEndPoint.Address + "  已连接", LogerType.Info);

            Uri uri = request.Url;
            string domain = uri.Authority;
            string urlPath = uri.AbsolutePath;
            string query = uri.Query;

            string responseString = "";


            if ((responseString = Router.SearchRouter(urlPath)) == null)
                return null;

            //响应数据流
            string context = "";

            RequestType requestType = CheckFileType(responseString);

            switch (requestType)
            {
                case RequestType.HTML:
                    context = CompileMain();
                    break;
                case RequestType.JSON:
                    context = Common.manager.GetModel().ToJsonString();
                    break;
                default:
                    context = FileHelper.ReadFile(responseString);
                    break;
            }
            return context;
        }
        
        private static string CompileMain(string ID)
        {
            if (ID == null)
                return CompileMain();
            return CompileMain();
        }
        private static string CompileMain()
        {
            string index = FileHelper.ReadFile("resource/main.html");
            string backgroundUrl = "static/img/" + DateTime.Now.Day % 15 + 1 + ".jpg";
            index = index.Replace("{% backgroundUrl %}", backgroundUrl);
            index = index.Replace("{% SModel %}", Common.manager.GetModel().ToTypedString());
            return index;
        }

        private static RequestType CheckFileType(string path)
        {
            path = path.ToLower();
            if (path.IndexOf(".htm") >= 0)
                return RequestType.HTML;
            if (path.IndexOf(".js") >= 0)
                return RequestType.JS;
            if (path.IndexOf(".css") >= 0)
                return RequestType.CSS;
            if (path.IndexOf(".jpg") >= 0)
                return RequestType.IMG;
            return RequestType.JSON;
        }
        private static Dictionary<string, string> CheckQuery(string query)
        {
            Dictionary<string, string> querys = new Dictionary<string, string>();
            foreach (string item in RegexHelper.MatchObj(RegexHelper.queryPattern, query))
            {
                string[] temp = item.Split('=');
                querys.Add(temp[0], temp[1]);
            }
            return querys;
        }
    }
    enum RequestType { HTML, JS, CSS, IMG, JSON };
}