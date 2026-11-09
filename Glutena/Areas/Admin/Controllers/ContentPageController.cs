using Glutena.Data;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ContentPageController : Controller
    {
        private readonly GlutenaDbContext _context;

        public ContentPageController(GlutenaDbContext context)
        {
            _context = context;
        }

        // ================= SAYFALAR =================

        // Tüm sayfaları listele
        public async Task<IActionResult> Index()
        {
            var pages = await _context.ContentPages
                .Include(p => p.Sections)
                .OrderBy(p => p.Title)
                .ToListAsync();
            return View(pages);
        }

        // Yeni sayfa formu
        public IActionResult Create()
        {
            return View("Edit", new ContentPage());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ContentPage model)
        {
            model.Slug = (model.Slug ?? "").Trim().ToLowerInvariant();
            ModelState.Remove(nameof(ContentPage.Sections));

            if (await _context.ContentPages.AnyAsync(p => p.Slug == model.Slug))
                ModelState.AddModelError(nameof(ContentPage.Slug), "Bu adres başka bir sayfada kullanılıyor.");

            if (!ModelState.IsValid)
                return View("Edit", model);

            _context.ContentPages.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "Sayfa oluşturuldu. Şimdi bölüm ekleyebilirsiniz.";
            return RedirectToAction("Edit", new { id = model.Id });
        }

        // Sayfayı ve bölümlerini düzenle
        public async Task<IActionResult> Edit(int id)
        {
            var page = await _context.ContentPages
                .Include(p => p.Sections.OrderBy(s => s.Order))
                .FirstOrDefaultAsync(p => p.Id == id);
            if (page == null) return NotFound();
            return View(page);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ContentPage model)
        {
            var existing = await _context.ContentPages
                .Include(p => p.Sections.OrderBy(s => s.Order))
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existing == null) return NotFound();

            model.Slug = (model.Slug ?? "").Trim().ToLowerInvariant();
            ModelState.Remove(nameof(ContentPage.Sections));

            if (await _context.ContentPages.AnyAsync(p => p.Slug == model.Slug && p.Id != id))
                ModelState.AddModelError(nameof(ContentPage.Slug), "Bu adres başka bir sayfada kullanılıyor.");

            if (!ModelState.IsValid)
            {
                model.Id = id;
                model.Sections = existing.Sections;
                return View(model);
            }

            existing.Slug = model.Slug;
            existing.Title = model.Title;
            existing.Subtitle = model.Subtitle;
            existing.Icon = model.Icon;
            existing.Intro = model.Intro;
            existing.Footnote = model.Footnote;
            existing.IsActive = model.IsActive;
            existing.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Sayfa bilgileri kaydedildi.";
            return RedirectToAction("Edit", new { id });
        }

        // Sayfayı bölümleriyle birlikte sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            var page = await _context.ContentPages
                .Include(p => p.Sections)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (page != null)
            {
                _context.ContentSections.RemoveRange(page.Sections);
                _context.ContentPages.Remove(page);
                await _context.SaveChangesAsync();
                TempData["Success"] = $"\"{page.Title}\" sayfası silindi.";
            }
            return RedirectToAction("Index");
        }

        // ================= BÖLÜMLER =================

        // Yeni bölüm formu
        public async Task<IActionResult> SectionCreate(int pageId)
        {
            var page = await _context.ContentPages.FindAsync(pageId);
            if (page == null) return NotFound();

            var lastOrder = await _context.ContentSections
                .Where(s => s.ContentPageId == pageId)
                .Select(s => (int?)s.Order)
                .MaxAsync() ?? 0;

            ViewBag.PageTitle = page.Title;
            return View("SectionForm", new ContentSection { ContentPageId = pageId, Order = lastOrder + 1 });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectionCreate(ContentSection model)
        {
            ModelState.Remove(nameof(ContentSection.ContentPage));
            var page = await _context.ContentPages.FindAsync(model.ContentPageId);
            if (page == null) return NotFound();

            if (!ModelState.IsValid)
            {
                ViewBag.PageTitle = page.Title;
                return View("SectionForm", model);
            }

            _context.ContentSections.Add(model);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Bölüm eklendi.";
            return RedirectToAction("Edit", new { id = model.ContentPageId });
        }

        // Bölümü düzenle
        public async Task<IActionResult> SectionEdit(int id)
        {
            var section = await _context.ContentSections
                .Include(s => s.ContentPage)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (section == null) return NotFound();

            ViewBag.PageTitle = section.ContentPage?.Title;
            return View("SectionForm", section);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectionEdit(int id, ContentSection model)
        {
            ModelState.Remove(nameof(ContentSection.ContentPage));
            var existing = await _context.ContentSections
                .Include(s => s.ContentPage)
                .FirstOrDefaultAsync(s => s.Id == id);
            if (existing == null) return NotFound();

            if (!ModelState.IsValid)
            {
                model.Id = id;
                model.ContentPageId = existing.ContentPageId;
                ViewBag.PageTitle = existing.ContentPage?.Title;
                return View("SectionForm", model);
            }

            existing.Icon = model.Icon;
            existing.Title = model.Title;
            existing.ShortTitle = model.ShortTitle;
            existing.Body = model.Body;
            existing.IsActive = model.IsActive;
            existing.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["Success"] = "Bölüm kaydedildi.";
            return RedirectToAction("Edit", new { id = existing.ContentPageId });
        }

        // Bölümü sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectionDelete(int id)
        {
            var section = await _context.ContentSections.FindAsync(id);
            if (section == null) return NotFound();

            var pageId = section.ContentPageId;
            _context.ContentSections.Remove(section);
            await _context.SaveChangesAsync();
            TempData["Success"] = "Bölüm silindi.";
            return RedirectToAction("Edit", new { id = pageId });
        }

        // Bölümü yayından kaldır / yayına al
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectionToggle(int id)
        {
            var section = await _context.ContentSections.FindAsync(id);
            if (section == null) return NotFound();

            section.IsActive = !section.IsActive;
            section.UpdatedDate = DateTime.Now;
            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = section.ContentPageId });
        }

        // Bölümü yukarı (-1) ya da aşağı (+1) taşı
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SectionMove(int id, int direction)
        {
            var section = await _context.ContentSections.FindAsync(id);
            if (section == null) return NotFound();

            var siblings = await _context.ContentSections
                .Where(s => s.ContentPageId == section.ContentPageId)
                .OrderBy(s => s.Order)
                .ToListAsync();

            // Sıra numaralarını 1, 2, 3... olarak düzelt
            for (int i = 0; i < siblings.Count; i++)
                siblings[i].Order = i + 1;

            var index = siblings.FindIndex(s => s.Id == id);
            var target = index + direction;

            if (target >= 0 && target < siblings.Count)
            {
                (siblings[index].Order, siblings[target].Order) = (siblings[target].Order, siblings[index].Order);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = section.ContentPageId });
        }

        // ================= RESİM YÜKLEME =================

        // Yazı kutusundaki "Resim ekle" butonu resmi buraya gönderir.
        // Resim wwwroot/img/sayfalar klasörüne kaydedilir, adresi yazı kutusuna geri döner.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(5 * 1024 * 1024)]
        public async Task<IActionResult> UploadImage(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest(new { error = "Dosya seçilmedi." });

            if (file.Length > 5 * 1024 * 1024)
                return BadRequest(new { error = "Resim en fazla 5 MB olabilir." });

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".webp", ".gif" };
            var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (!allowedExtensions.Contains(extension))
                return BadRequest(new { error = "Sadece JPG, PNG, WEBP ve GIF resimleri yüklenebilir." });

            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "sayfalar");
            Directory.CreateDirectory(folder);

            var fileName = Guid.NewGuid().ToString() + extension;
            using (var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return Json(new { location = "/img/sayfalar/" + fileName });
        }

    }
}