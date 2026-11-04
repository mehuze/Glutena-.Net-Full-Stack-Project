using Glutena.Data;
using Glutena.Data.Abstract;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Controllers
{
    public class ExperienceController : Controller
    {
        private readonly IGenericRepository<Experience> _experienceRepository;
        private readonly GlutenaDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public ExperienceController(
            IGenericRepository<Experience> experienceRepository,
            GlutenaDbContext context,
            UserManager<IdentityUser> userManager)
        {
            _experienceRepository = experienceRepository;
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var experiences = await _context.Experiences
                .Include(e => e.Product)
                .Where(e => e.IsApproved)
                .OrderByDescending(e => e.CreatedDate)
                .ToListAsync();
            return View(experiences);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Experience model)
        {
            if (!ModelState.IsValid)
            {
                return Content("Model geçersiz");
            }

            // Kara listeyi veritabanından çek
            var blacklist = await _context.BlacklistWords
                .Select(b => b.Word)
                .ToListAsync();

            var text = (model.Title + " " + model.Content).ToLower();
            var cleanText = new string(text.Where(c => char.IsLetterOrDigit(c)).ToArray());

            var hasInappropriate = blacklist.Any(word => text.Contains(word));
            var hasInappropriateClean = blacklist.Any(word =>
                cleanText.Contains(word.Replace(" ", "").ToLower()));

            if (hasInappropriate || hasInappropriateClean)
            {
                TempData["Error"] = "Yorumunuz uygunsuz ifadeler içeriyor. Lütfen düzeltin.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            // Giriş yapmış kullanıcının ID'sini al
            // Giriş yapmış kullanıcının ID'sini al (giriş yapılmamışsa boş kalır)
            model.UserId = _userManager.GetUserId(User);

            model.IsApproved = false;
            model.IsActive = true;
            model.CreatedDate = DateTime.Now;
            await _experienceRepository.AddAsync(model);
            await _experienceRepository.SaveAsync();

            TempData["Success"] = "Yorumunuz alındı, onay bekliyor.";
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}