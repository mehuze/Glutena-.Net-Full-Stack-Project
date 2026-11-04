using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Glutena.Data;
using Glutena.Models.Entities;

namespace Glutena.Controllers
{
    public class HomeController : Controller
    {
        private readonly GlutenaDbContext _context;

        public HomeController(GlutenaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Take(4)
                .ToListAsync();

            var blogs = await _context.Blogs
                .OrderByDescending(b => b.CreatedDate)
                .Take(3)
                .ToListAsync();

            ViewBag.Products = products;
            ViewBag.Blogs = blogs;
            return View();
        }
        // İletişim sayfasını aç
        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        // Formdan gelen mesajı kaydet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contact(ContactMessage model)
        {
            // Konu sadece formdaki seçeneklerden biri olabilir
            var allowedSubjects = new[] { "Genel soru", "Ürün önerisi", "Hatalı bilgi bildirimi", "Marka işbirliği" };
            if (!allowedSubjects.Contains(model.Subject))
                model.Subject = "Genel soru";

            if (!ModelState.IsValid)
                return View(model);

            model.IsRead = false;
            model.CreatedDate = DateTime.Now;

            _context.ContactMessages.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Mesajınız alındı. En kısa sürede size dönüş yapacağız.";
            return RedirectToAction("Contact");
        }
        public IActionResult Test()
        {
            return View();
        }
        // Eski adres: yeni dinamik Çölyak sayfasına yönlendirir
        public IActionResult Colyak()
        {
            return RedirectToAction("Detail", "Page", new { slug = "colyak" });
        }

        // Yeni tanı alanlar için başlangıç rehberi
        // Eski adres: yeni dinamik rehber sayfasına yönlendirir
        public IActionResult YeniTani()
        {
            return RedirectToAction("Detail", "Page", new { slug = "yeni-tani" });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? code)
        {
            ViewBag.Code = code ?? 500;
            return View();
        }
    }
}