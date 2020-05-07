using System.Collections.Generic;

namespace rainbow
{
    /// <summary>
    /// 全局Url路径路由表
    /// </summary>
    public class Router
    {
        static  Dictionary<string, string> RouterLib = new Dictionary<string, string>();

        static public int GetCount() => RouterLib.Count;
        static public void Add(string router, string source)
        {
            RouterLib.Add(router, source);
        }
        static public void Add(string ymlString){
            if(RegexHelper.Test(RegexHelper.routerPattern, ymlString)){
                string[] sp = ymlString.Split(':');
                Add(sp[0].Trim(), sp[1].Trim());
            }
        }
        static public string SearchRouter(string router)
        {

            Dictionary<string, string> temp =  RouterLib;
            if (RouterLib.ContainsKey(router))
                return RouterLib[router];
            else
                return null;
        }
        static public void init(){
            string[] list = FileHelper.ReadFileLine("data/RouterList.yml");;
            foreach(var item in list){
                Add(item);
            }
        }
    }
}