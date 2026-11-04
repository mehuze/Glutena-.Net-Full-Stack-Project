using Glutena.Data;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogController : Controller
    {
        private readonly GlutenaDbContext _context;

        public BlogController(GlutenaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var blogs = await _context.Blogs.ToListAsync();
            return View(blogs);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Blog blog, IFormFile? photoFile)
        {
            if (photoFile != null)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }
                blog.PhotoPath = "/img/" + fileName;
            }
            if (ModelState.IsValid)
            {
                _context.Blogs.Add(blog);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(blog);
        }

        // 1. Aşama: Düzenleme sayfasını aç (EKSİK OLAN KISIM BUYDU)
        public async Task<IActionResult> Edit(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);
            if (blog == null) return NotFound();
            return View(blog);
        }

        // 2. Aşama: Değişiklikleri kaydet
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Blog blog, IFormFile? photoFile)
        {
            // Veritabanındaki asıl kaydı bul
            var existing = await _context.Blogs.FindAsync(id);
            if (existing == null) return NotFound();

            if (!ModelState.IsValid)
            {
                blog.PhotoPath = existing.PhotoPath;
                return View(blog);
            }

            // Sadece formdaki alanları güncelle (resim ve oluşturulma tarihi korunur)
            existing.BlogName = blog.BlogName;
            existing.Content = blog.Content;
            existing.IsActive = blog.IsActive;
            existing.UpdatedDate = DateTime.Now;

            // Yeni resim seçildiyse onu kaydet, seçilmediyse eskisi kalır
            if (photoFile != null && photoFile.Length > 0)
            {
                var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await photoFile.CopyToAsync(stream);
                }
                existing.PhotoPath = "/img/" + fileName;
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var blog = await _context.Blogs.FindAsync(id);

            if (blog != null)
            {
                _context.Blogs.Remove(blog);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Index");
        }
    }
}