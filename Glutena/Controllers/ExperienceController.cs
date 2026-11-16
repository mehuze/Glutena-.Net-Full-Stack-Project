using System.Globalization;
using System.Text.RegularExpressions;
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

        // Türkçe büyük/küçük harf için (İ → i, I → ı)
        private static readonly CultureInfo Turkish = new CultureInfo("tr-TR");

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

            // Yorumun başlığında veya metninde yasaklı kelime var mı?
            if (ContainsBlacklistedWord(model.Title + " " + model.Content, blacklist))
            {
                TempData["Error"] = "Yorumunuz uygunsuz ifadeler içeriyor. Lütfen düzeltin.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

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

        // ================= KARA LİSTE KONTROLÜ =================
        // Metni kelimelere ayırır ve her kelimeyi kara listeyle karşılaştırır.
        // - 5 harf ve üzeri yasaklı kelimeler: kelimenin BAŞINDA geçerse yakalanır.
        //   Türkçe ekler için: "şerefsiz" yasaksa "şerefsizler" de yakalanır.
        // - 5 harften kısa yasaklı kelimeler: sadece TEK BAŞINA yazılırsa yakalanır.
        //   Böylece kısa bir kelime, masum bir kelimenin içinde geçince yorum engellenmez.
        // - Birden fazla kelimelik ifadeler: ifadenin tamamı aranır.
        private static bool ContainsBlacklistedWord(string text, List<string> blacklist)
        {
            // Metni küçük harfe çevir ve harf/rakam dışındaki her şeyden böl
            var words = Regex.Split(text.ToLower(Turkish), @"[^\p{L}\p{N}]+")
                .Where(w => w.Length > 0)
                .ToList();

            var joinedText = " " + string.Join(" ", words) + " ";

            foreach (var entry in blacklist)
            {
                var banned = Regex.Replace(entry.Trim().ToLower(Turkish), @"\s+", " ");
                if (banned.Length == 0) continue;

                // Birden fazla kelimelik ifade
                if (banned.Contains(' '))
                {
                    if (joinedText.Contains(" " + banned + " ")) return true;
                    continue;
                }

                bool found = banned.Length >= 5
                    ? words.Any(w => w.StartsWith(banned, StringComparison.Ordinal))
                    : words.Any(w => w == banned);

                if (found) return true;
            }

            return false;
        }
    }
}