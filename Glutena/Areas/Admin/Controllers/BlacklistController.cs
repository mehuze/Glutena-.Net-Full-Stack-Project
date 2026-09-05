using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glutena.Data;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Authorization;
namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlacklistController : Controller
    {
        private readonly GlutenaDbContext _context;

        public BlacklistController(GlutenaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var words = await _context.BlacklistWords.ToListAsync();
            return View(words);
        }

        [HttpPost]
        public async Task<IActionResult> Add(string word)
        {
            if (!string.IsNullOrEmpty(word))
            {
                _context.BlacklistWords.Add(new BlacklistWord
                {
                    Word = word.ToLower().Trim()
                });
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var word = await _context.BlacklistWords.FindAsync(id);
            if (word != null)
            {
                _context.BlacklistWords.Remove(word);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
    }
}