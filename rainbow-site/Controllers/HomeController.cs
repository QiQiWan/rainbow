using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rainbow_site.Models;

namespace rainbow_site.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
            Common.UpdateDate();
            BGImg = Common.backgroundImg;
        }
        public void LanguageChanges(string lang)
        {
            switch (lang)
            {
                case "en":
                    ViewData["IndexUrl"] = "/Index/en/";
                    ViewData["AboutUrl"] = "/About/en/";
                    ViewData["AboutTitle"] = "About";
                    ViewData["LangDes"] = "语言: 英语";
                    break;
                default:
                    ViewData["IndexUrl"] = "/";
                    ViewData["AboutUrl"] = "/About/";
                    ViewData["AboutTitle"] = "关于";
                    ViewData["LangDes"] = "Language: English";
                    break;
            }
        }
        public static string BGImg;
        public IActionResult Index(string lang)
        {
            ViewData["BGImg"] = BGImg;
            LanguageChanges(lang);

            if (lang == "en")
                return View("Index_en");

            return View();
        }
        public IActionResult About(string lang)
        {
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
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
