using Glutena.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Controllers
{
    // Admin panelinden yönetilen bilgi sayfalarını gösterir
    public class PageController : Controller
    {
        private readonly GlutenaDbContext _context;

        public PageController(GlutenaDbContext context)
        {
            _context = context;
        }

        // Örnek adres: https://localhost:7216/sayfa/yeni-tani
        [Route("sayfa/{slug}")]
        public async Task<IActionResult> Detail(string slug)
        {
            var page = await _context.ContentPages
                .Include(p => p.Sections
                    .Where(s => s.IsActive)
                    .OrderBy(s => s.Order))
                .FirstOrDefaultAsync(p => p.Slug == slug && p.IsActive);

            if (page == null)
                return NotFound();

            return View(page);
        }
    }
}