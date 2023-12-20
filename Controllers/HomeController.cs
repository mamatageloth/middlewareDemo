using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using middlewareDemo.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace middlewareDemo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        //set session object
        public IActionResult Index()
        {
            HttpContext.Session.SetString("Name", "Vrushali");
            HttpContext.Session.SetInt32("Age",32);
            return View();
        }
        //get session object
        public IActionResult Privacy()
        {
            ViewBag.name = HttpContext.Session.GetString("Name");
            ViewBag.age = HttpContext.Session.GetInt32("Age");
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
