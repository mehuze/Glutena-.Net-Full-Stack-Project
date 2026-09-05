using Glutena.Data;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ExperienceController : Controller
    {
        private readonly GlutenaDbContext _context;

        public ExperienceController(GlutenaDbContext context)
        {
            _context = context;
        }

        // Tüm deneyimleri listele
        public async Task<IActionResult> Index()
        //Veritabanındaki tüm deneyimleri çekiyor. Include(e => e.Product) ile hangi ürüne ait olduğunu da getiriyor. En yeni yorum üstte görünüyor.
        {
            var experiences = await _context.Experiences
                .Include(e => e.Product)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();
            return View(experiences);
        }

        // Onayla
        public async Task<IActionResult> Approve(int id)
        {//Seçilen yorumun IsApproved değerini false'dan true'ya çeviriyor. Bundan sonra o yorum sitede görünmeye başlıyor.
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                experience.IsApproved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Sil
        public async Task<IActionResult> Delete(int id)
        {//Spam veya uygunsuz yorumu tamamen siliyor.
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                _context.Experiences.Remove(experience);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}