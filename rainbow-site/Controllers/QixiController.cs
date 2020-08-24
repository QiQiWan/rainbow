using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rainbow_site.Models;
using System.Collections.Generic;

namespace rainbow_site.Controllers
{
    public class QixiController : Controller
    {

        public QixiController()
        {
        }

        public IActionResult Index()
        {
            ViewData["BGImg"] = "/img/Qixi.jpg";
            return View();
        }
        public IActionResult Type1()
        {
            // 指定母版
            ViewData["Layout"] = "_QixiLayout";
            return View();
        }
        public IActionResult Type2()
        {
            // 指定母版
            ViewData["Layout"] = "_QixiLayout";
            ViewData["MyLove"] = SetDefaultValue(Request, key: "MyLove", defaultValue: "My Love");
            ViewData["Year"] = SetDefaultValue(Request, key: "Year", defaultValue: "2017");
            ViewData["Month"] = SetDefaultValue(Request, key: "Month", defaultValue: "7");
            ViewData["Day"] = SetDefaultValue(Request, key: "Day", defaultValue: "25");
            ViewData["Hour"] = SetDefaultValue(Request, key: "Hour", defaultValue: "20");
            ViewData["Minute"] = SetDefaultValue(Request, key: "Minute", defaultValue: "0");
            ViewData["Second"] = SetDefaultValue(Request, key: "Second", defaultValue: "0");
            ViewData["MyName"] = SetDefaultValue(Request, key: "MyName", defaultValue: "EatRice.Me");

            return View();
        }
        public IActionResult Type3()
        {
            // 指定母版
            ViewData["Layout"] = "_QixiLayout";
            List<string> statements = new List<string>();

            ViewData["MyLove"] = SetDefaultValue(Request, key: "MyLove", defaultValue: "My Love");
            ViewData["Year"] = SetDefaultValue(Request, key: "Year", defaultValue: "2019");
            ViewData["Month"] = SetDefaultValue(Request, key: "Month", defaultValue: "7");
            ViewData["Day"] = SetDefaultValue(Request, key: "Day", defaultValue: "25");
            ViewData["Hour"] = SetDefaultValue(Request, key: "Hour", defaultValue: "20");
            ViewData["Minute"] = SetDefaultValue(Request, key: "Minute", defaultValue: "0");
            ViewData["Second"] = SetDefaultValue(Request, key: "Second", defaultValue: "0");
            ViewData["MyName"] = SetDefaultValue(Request, key: "MyName", defaultValue: "EatRice.Me");
            ViewData["statements"] = SetStatements(Request);
            return View();
        }

        public IActionResult Type4()
        {
            // 指定母版
            ViewData["Layout"] = "_QixiLayout";

            ViewData["MyLove"] = SetDefaultValue(Request, key: "MyLove", defaultValue: "My Love");
            ViewData["MyName"] = SetDefaultValue(Request, key: "MyName", defaultValue: "EatRice.Me");
            ViewData["Year"] = SetDefaultValue(Request, key: "Year", defaultValue: "2019");
            ViewData["Month"] = SetDefaultValue(Request, key: "Month", defaultValue: "7");
            ViewData["Day"] = SetDefaultValue(Request, key: "Day", defaultValue: "25");
            ViewData["Hour"] = SetDefaultValue(Request, key: "Hour", defaultValue: "20");
            ViewData["Minute"] = SetDefaultValue(Request, key: "Minute", defaultValue: "0");
            ViewData["Second"] = SetDefaultValue(Request, key: "Second", defaultValue: "0");
            ViewData["S1"] = SetDefaultValue(Request, key: "S1", defaultValue: "自见过你那一天，我学会了失眠，不为你的容颜，只忆你的笑脸");
            ViewData["S2"] = SetDefaultValue(Request, key: "S2", defaultValue: "若不能分分钟见到你的身影，我便会惶恐不安，你的出现，占满了我的视线，我愿跟随你永远。");
            ViewData["S3"] = SetDefaultValue(Request, key: "S3", defaultValue: "之前我不相信一见钟情，但是见到你的那一刻，我否定了我的看法。");
            ViewData["S4"] = SetDefaultValue(Request, key: "S4", defaultValue: "我的心为你沦陷，从此只为你跳动。");
            ViewData["S5"] = SetDefaultValue(Request, key: "S5", defaultValue: "有一种爱的感觉，叫感同身受。");
            ViewData["S6"] = SetDefaultValue(Request, key: "S6", defaultValue: "有一种爱的默契，叫心有灵犀。");
            ViewData["S7"] = SetDefaultValue(Request, key: "S7", defaultValue: "有一种爱的承诺，叫天长地久。");
            ViewData["S8"] = SetDefaultValue(Request, key: "S8", defaultValue: "有一种爱的方式，是执子之手,与子携老。");
            ViewData["S9"] = SetDefaultValue(Request, key: "S9", defaultValue: "我一直在");
            return View();
        }
        private List<string> SetStatements(HttpRequest request)
        {
            if (!request.Query.ContainsKey("statements"))
            {
                return null;
            }
            List<string> statements = new List<string>();
            string statement = System.Web.HttpUtility.UrlDecode(request.Query["statements"].ToString());
            statements = new List<string>(statement.Split("'"));
            return statements;
        }
        private string SetDefaultValue(HttpRequest request, string key, string defaultValue)
        {
            return request.Query.ContainsKey(key) ? System.Web.HttpUtility.UrlDecode(request.Query[key].ToString()) : defaultValue;
        }
    }
}