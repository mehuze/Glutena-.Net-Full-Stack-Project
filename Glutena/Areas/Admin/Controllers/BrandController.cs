using Glutena.Data;
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BrandController : Controller
    {
        private readonly GlutenaDbContext _context;

        public BrandController(GlutenaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var brands = await _context.Brands.ToListAsync();
            return View(brands);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Brand brand)
        {
            if (ModelState.IsValid)
            {
                _context.Brands.Add(brand);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(brand);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return NotFound();
            return View(brand);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, Brand brand)
        {
            var existing = await _context.Brands.FindAsync(id);
            if (existing == null) return NotFound();

            if (!ModelState.IsValid)
                return View(brand);

            // Sadece adı güncelle, oluşturulma tarihi korunsun
            existing.Name = brand.Name;
            existing.UpdatedDate = DateTime.Now;

            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var brand = await _context.Brands.FindAsync(id);
            if (brand == null) return RedirectToAction("Index");

            // Bu markaya bağlı ürün var mı? Varsa silme, uyarı göster.
            var productCount = await _context.Products.CountAsync(p => p.BrandId == id);
            if (productCount > 0)
            {
                TempData["Error"] = $"\"{brand.Name}\" markasına bağlı {productCount} ürün var. Önce bu ürünleri silin ya da başka bir markaya taşıyın.";
                return RedirectToAction("Index");
            }

            _context.Brands.Remove(brand);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}