using Glutena.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly GlutenaDbContext _context;

        public MessageController(GlutenaDbContext context)
        {
            _context = context;
        }

        // Tüm mesajları listele: okunmamışlar üstte, en yeniler önce
        public async Task<IActionResult> Index()
        {
            var messages = await _context.ContactMessages
                .OrderBy(m => m.IsRead)
                .ThenByDescending(m => m.CreatedDate)
                .ToListAsync();
            return View(messages);
        }

        // Okundu / okunmadı olarak işaretle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleRead(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                message.IsRead = !message.IsRead;
                message.UpdatedDate = DateTime.Now;
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // Mesajı sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var message = await _context.ContactMessages.FindAsync(id);
            if (message != null)
            {
                _context.ContactMessages.Remove(message);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}