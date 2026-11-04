using Glutena.Data;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

        // Tüm deneyimleri listele (en yeni üstte)
        public async Task<IActionResult> Index()
        {
            var experiences = await _context.Experiences
                .Include(e => e.Product)
                .Include(e => e.User)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();
            return View(experiences);
        }

        // Onayla: yorum sitede görünmeye başlar
        public async Task<IActionResult> Approve(int id)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                experience.IsApproved = true;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Glutena yanıtını kaydet (boş gönderilirse yanıt silinir)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Reply(int id, string? adminReply)
        {
            var experience = await _context.Experiences.FindAsync(id);
            if (experience != null)
            {
                var text = adminReply?.Trim();

                if (string.IsNullOrEmpty(text))
                {
                    experience.AdminReply = null;
                    experience.AdminReplyDate = null;
                }
                else
                {
                    if (text.Length > 1000) text = text.Substring(0, 1000);
                    experience.AdminReply = text;
                    experience.AdminReplyDate = DateTime.Now;
                }

                await _context.SaveChangesAsync();
                TempData["Success"] = "Yanıt kaydedildi.";
            }
            return RedirectToAction("Index");
        }

        // Sil: spam veya uygunsuz yorumu tamamen siler
        public async Task<IActionResult> Delete(int id)
        {
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