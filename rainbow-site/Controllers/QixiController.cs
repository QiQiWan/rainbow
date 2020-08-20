using System.Diagnostics;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using rainbow_site.Models;

namespace rainbow_site.Controllers
{
    public class QixiController : Controller
    {

        public QixiController(){

        }

        public IActionResult Index(){
            return View();
        }
    }
}