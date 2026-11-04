using Glutena.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly GlutenaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public DashboardController(GlutenaDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            // Deneyimler
            ViewBag.TotalExperiences = await _context.Experiences.CountAsync();
            ViewBag.PendingExperiences = await _context.Experiences.CountAsync(e => !e.IsApproved);
            ViewBag.RepliedExperiences = await _context.Experiences.CountAsync(e => e.AdminReply != null);
            ViewBag.UnansweredExperiences = await _context.Experiences.CountAsync(e => e.IsApproved && e.AdminReply == null);

            // Mesajlar
            ViewBag.UnreadMessages = await _context.ContactMessages.CountAsync(m => !m.IsRead);

            // Üyeler ve ürünler
            ViewBag.TotalUsers = await _userManager.Users.CountAsync();
            ViewBag.TotalProducts = await _context.Products.CountAsync();

            // Son işler: onay bekleyen son 5 deneyim ve okunmamış son 5 mesaj
            ViewBag.LatestPending = await _context.Experiences
                .Include(e => e.Product)
                .Where(e => !e.IsApproved)
                .OrderByDescending(e => e.CreatedDate)
                .Take(5)
                .ToListAsync();

            ViewBag.LatestMessages = await _context.ContactMessages
                .Where(m => !m.IsRead)
                .OrderByDescending(m => m.CreatedDate)
                .Take(5)
                .ToListAsync();

            return View();
        }
    }
}