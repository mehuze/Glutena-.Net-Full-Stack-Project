using Glutena.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Controllers
{
    [Authorize] // Sadece giriş yapmış kullanıcılar girebilir
    public class ProfileController : Controller
    {
        private readonly GlutenaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ProfileController(GlutenaDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction("Login", "Account");

            // Bu kullanıcının yazdığı yorumlar, en yenisi üstte
            var myExperiences = await _context.Experiences
                .Include(e => e.Product)
                .Where(e => e.UserId == user.Id)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();

            ViewBag.Email = user.Email;
            return View(myExperiences);
        }
    }
}