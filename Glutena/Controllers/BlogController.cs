using Glutena.Data;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Controllers
{
    public class BlogController : Controller
    {
        private readonly GlutenaDbContext _context;

        public BlogController(GlutenaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs
                .Where(x => x.IsActive)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();

            //Database'den Blogs tablosunu alır
            //2️ IsActive = true olanları getirir
            //3️ En yeni blog üstte olacak şekilde sıralar
            // 4️ Listeyi View'e gönderir
            return View(blogs);
        }

        public async Task<IActionResult> Detail(int id)
        {
            var blog = await _context.Blogs
                .FirstOrDefaultAsync(x => x.Id == id);
            if (blog == null)
                return NotFound();

            // Diğer blogları getir
            ViewBag.OtherBlogs = await _context.Blogs
                .Where(b => b.Id != id && b.IsActive)
                .OrderByDescending(b => b.CreatedDate)
                .Take(3)
                .ToListAsync();

            return View(blog);
        }
    }
}