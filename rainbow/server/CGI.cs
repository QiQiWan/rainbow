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
            string urlPath = uri.AbsolutePath;
            string query = uri.Query;

            string id = CheckQuery(query, "ID");
            string responseString = Router.SearchRouter(urlPath);
            if (responseString == null)
                responseString = "/GetJson/";
            string context;

            //线程锁
            lock (Common.Lock)
            {
                switch (responseString)
                {
                    case "/GetJson/":
                        context = Common.manager.GetModel(id).ToJsonString();
                        break;
                    case "/songs/":
                        context = Common.manager.GetModel(SModelType.Songs).ToJsonString();
                        break;
                    case "/movies/":
                        context = Common.manager.GetModel(SModelType.Movies).ToJsonString();
                        break;
                    default:
                        context = Common.manager.GetModel(SModelType.Reading).ToJsonString();
                        break;
                }
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
            string index = FileHelper.ReadFile("template/main.html");
            string backgroundUrl = "img/" + (DateTime.Now.Day % 15 + 1) + ".jpg";
            index = index.Replace("{% backgroundUrl %}", backgroundUrl);
            index = index.Replace("{% SModel %}", Common.manager.GetModel().ToTypedString());
            return index;
        }
        private static string CheckQuery(string query, string eleName)
        {
            return RegexHelper.MatchQueEle(RegexHelper.queryPattern, eleName, query);
        }
    }
}