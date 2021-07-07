using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rainbow_site.Models;

namespace rainbow_site.Controllers
{
    public class HomeController : Controller
    {

        // 构造注入请求上下文
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeController(IHttpContextAccessor httpContextAccessor)
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
        public IActionResult Index(string lang)
        {
            ViewData["BGImg"] = BGImg;

            lang = lang.ToLower();

            LanguageChanges(lang);

            if (lang == "en")
                return View("Index_en");

            return View();
        }
        public IActionResult About(string lang)
        {
            lang = lang.ToLower();
            ViewData["BGImg"] = BGImg;
            LanguageChanges(lang);

            if (lang == "en")
                return View("About_en");

            return View();
        }

        public string GetJson()
        {
            string ID = Request.Query["ID"];
            string url = $"https://api.eatrice.top/?ID={ID}";

            string Json = WebHelper.Request(url);
            return Json;
        }

        public IActionResult Format(string lang)
        {
            ViewData["BGImg"] = BGImg;
            LanguageChanges(lang);

            return View("add");
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
