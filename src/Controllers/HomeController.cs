using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OcelotWeb.Models;

namespace OcelotWeb.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            var model = new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return base.View(model);
        }
    }
}
