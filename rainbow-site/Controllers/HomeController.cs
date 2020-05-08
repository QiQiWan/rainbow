using System.Diagnostics;
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
        public static string BGImg;
        public IActionResult Index()
        {
            ViewData["BGImg"] = BGImg;
            return View();
        }
        public IActionResult Doc()
        {
            ViewData["BGImg"] = BGImg;
            return View();
        }

        public string GetJson(){

            string ID = Request.Query["ID"];
            string url = $"https://api.eatrice.top/?ID={ID}";

            string Json = WebHelper.Request(url);
            return Json;
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
