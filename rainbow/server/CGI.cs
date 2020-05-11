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
                    case "/GetAll/":
                        context = Common.manager.AllModelString;
                        break;
                    default:
                        context = Common.manager.GetModel(SModelType.Reading).ToJsonString();
                        break;
                }
            }
            return context;
        }
        private static string CheckQuery(string query, string eleName)
        {
            return RegexHelper.MatchQueEle(RegexHelper.queryPattern, eleName, query);
        }
    }
}