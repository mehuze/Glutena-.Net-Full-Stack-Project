using Microsoft.AspNetCore.Mvc;

namespace Glutena.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
