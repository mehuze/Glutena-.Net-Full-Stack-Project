using Glutena.Data;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        private readonly GlutenaDbContext _context;

        public ProductController(GlutenaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _context.Products
                .Include(x => x.Brand)
                .ToListAsync();
            return View(products);
        }

        public IActionResult Create()
        {
            FillLookups(null);
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile? photoFile, int[]? categoryIds)
        {
            if (!ModelState.IsValid)
            {
                FillLookups(categoryIds);
                return View(product);
            }

            // Resim seçildiyse kaydet (seçilmezse sorun değil)
            if (photoFile != null && photoFile.Length > 0)
                product.PhotoPath = await SavePhotoAsync(photoFile);

            product.CreatedDate = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();

            // Kategorileri bağla
            foreach (var categoryId in (categoryIds ?? Array.Empty<int>()).Distinct())
            {
                _context.ProductCategories.Add(new ProductCategory
                {
                    ProductId = product.Id,
                    CategoryId = categoryId
                });
            }
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (product == null) return NotFound();

            FillLookups(product.ProductCategories.Select(pc => pc.CategoryId).ToArray());
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Product product, IFormFile? photoFile, int[]? categoryIds)
        {
            // Veritabanındaki asıl ürünü bul
            var existing = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);
            if (existing == null) return NotFound();

            if (!ModelState.IsValid)
            {
                product.Id = id;
                product.PhotoPath = existing.PhotoPath;
                FillLookups(categoryIds);
                return View(product);
            }

            // Sadece formdaki alanları güncelle.
            // Formda olmayanlar (affiliate link, sponsorluk, tarih vb.) korunur.
            existing.ProductName = product.ProductName;
            existing.Description = product.Description;
            existing.GlutenStatus = product.GlutenStatus;
            existing.Slug = product.Slug;
            existing.Price = product.Price;
            existing.BrandId = product.BrandId;
            existing.UpdatedDate = DateTime.Now;

            // Yeni resim seçildiyse değiştir, seçilmediyse eskisi kalır
            if (photoFile != null && photoFile.Length > 0)
                existing.PhotoPath = await SavePhotoAsync(photoFile);

            // Kategorileri karşılaştır: kaldırılanları sil, yeni seçilenleri ekle
            var selected = (categoryIds ?? Array.Empty<int>()).Distinct().ToList();

            var toRemove = existing.ProductCategories
                .Where(pc => !selected.Contains(pc.CategoryId))
                .ToList();
            _context.ProductCategories.RemoveRange(toRemove);

            var current = existing.ProductCategories.Select(pc => pc.CategoryId).ToList();
            foreach (var categoryId in selected.Where(c => !current.Contains(c)))
            {
                _context.ProductCategories.Add(new ProductCategory
                {
                    ProductId = id,
                    CategoryId = categoryId
                });
            }

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product != null)
            {
                // Önce kategori bağlantılarını sil, yoksa veritabanı silmeye izin vermez
                _context.ProductCategories.RemoveRange(product.ProductCategories);
                _context.Products.Remove(product);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

        // ---- Yardımcı metotlar ----

        // Marka ve kategori listelerini sayfaya gönderir
        private void FillLookups(int[]? selectedCategoryIds)
        {
            ViewBag.Brands = _context.Brands.ToList();
            ViewBag.Categories = _context.Categories.ToList();
            ViewBag.SelectedCategories = (selectedCategoryIds ?? Array.Empty<int>()).ToList();
        }

        // Resmi wwwroot/img klasörüne kaydeder ve yolunu döndürür
        private async Task<string> SavePhotoAsync(IFormFile photoFile)
        {
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(photoFile.FileName);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/img", fileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await photoFile.CopyToAsync(stream);
            }
            return "/img/" + fileName;
        }
    }
}