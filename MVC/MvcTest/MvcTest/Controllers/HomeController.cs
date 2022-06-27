using Microsoft.AspNetCore.Mvc;
using MvcTest.Models;
using System.Diagnostics;

namespace MvcTest.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly EfCoreContext _db;

        public HomeController(ILogger<HomeController> logger, EfCoreContext db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Test(InputModel input)
        {
            var blogs = _db.Blog.ToList();

            TestViewModel model = new TestViewModel();
            model.Name = input.Id;

            ViewData["Show"] = input.Show;

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}