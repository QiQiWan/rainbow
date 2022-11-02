using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using rainbow_site.Models;

namespace rainbow_site.Controllers
{
    public class WeLMController : Controller
    {

        // 构造注入请求上下文
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeLMController(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            Common.UpdateDate();
            BGImg = Common.backgroundImg;
        }

        public void LanguageChanges(string lang)
        {
            switch (lang)
            {
                case "en":
                    NavModel.Culture = CultureLanguage.EN;
                    NavModel.Refresh();
                    ViewData["LangDes"] = "语言: 英语";
                    break;
                default:
                    NavModel.Culture = CultureLanguage.CH;
                    NavModel.Refresh();
                    ViewData["LangDes"] = "Language: English";
                    break;
            }
        }
        public static string BGImg;
        public IActionResult Index(string str)
        {
            ViewData["BGImg"] = BGImg;
            str = str.ToLower();
            LanguageChanges(str);

            return View();
        }

        [HttpPost]
        public IActionResult GetPara()
        {
            Dictionary<string, string> param = WeLM.GetParamPairs();
            param.Add("prompt", Request.Form["prompt"]);
            param.Add("max_tokens", Request.Form["maxT"]);

            Dictionary<string, string> header = new Dictionary<string, string>();
            // header.Add("Content-Type", "application/json");
            header.Add("Authorization", WeLM.Token);

            string paramStr = JsonConvert.SerializeObject(param);
            paramStr = paramStr.Replace("\"true\"", "true");
            paramStr = paramStr.Replace("\"1\"", "2");
            Console.WriteLine(paramStr);
            string result = WebHelper.Post(WeLM.url, paramStr, header);
            Console.WriteLine(result);

            return new JsonResult(result);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        // 错误页面重定向
        public IActionResult Error()
        {
            string url = _httpContextAccessor.HttpContext.Request.Host.Value;

            HttpContext.Response.Redirect("/");
            //return Redirect(url);
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
