using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glutena.Data;
using Glutena.Models.Entities;

namespace Glutena.Controllers
{
    public class HomeController : Controller
    {
        private readonly GlutenaDbContext _context;

        public HomeController(GlutenaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Take(4)
                .ToListAsync();

            var blogs = await _context.Blogs
                .OrderByDescending(b => b.CreatedDate)
                .Take(3)
                .ToListAsync();

            ViewBag.Products = products;
            ViewBag.Blogs = blogs;
            return View();
        }
        public IActionResult Contact()
        {
            return View();
        }
        public IActionResult Test()
        {
            return View();
        }
        public IActionResult Colyak()
        {
            return View();
        }
    }
}