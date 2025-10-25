using Learnify.UI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Learnify.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // E�er kullan�c� giri� yapm��sa ve Admin ise, dashboard'a y�nlendir
            if (User.Identity!.IsAuthenticated)
            {
                if (User.IsInRole("Admin"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                }
                else if (User.IsInRole("Instructor"))
                {
                    return RedirectToAction("Index", "Dashboard", new { area = "Instructor" });
                }
            }

            // De�ilse normal home sayfas�
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}