using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rainbow_site.Models;

namespace rainbow_site.Controllers
{
    public class QixiController : Controller
    {

        public QixiController()
        {
            ViewData["Layout"] = "_QixiLayout";
        }

        public IActionResult Index()
        {
            // 指定母版
            ViewData["Layout"] = "_QixiLayout";
            return View();
        }
        public IActionResult Type1()
        {
            // 指定母版
            ViewData["Layout"] = "_QixiLayout";
            return View("Index");
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
        private string SetDefaultValue(HttpRequest request, string key, string defaultValue)
        {
            return request.Query.ContainsKey(key) ? request.Query[key].ToString() : defaultValue;
        }
    }
}