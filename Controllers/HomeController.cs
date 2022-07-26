using Microsoft.AspNetCore.Mvc;
using PrettyWorld.Models;
using System.Diagnostics;

namespace PrettyWorld.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly PrettyWorldContext _db = new();

        public HomeController(ILogger<HomeController> logger, PrettyWorldContext context)
        {
            _logger = logger;
            _db = context;
        }

        public IActionResult Index()
        {
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