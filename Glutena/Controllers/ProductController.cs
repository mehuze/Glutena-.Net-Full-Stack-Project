using Glutena.Data;           // repository namespace
using Glutena.Data.Abstract;
using Glutena.Models;         // Product model namespace
using Glutena.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
namespace Glutena.Controllers
{

    public class ProductController : Controller
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly GlutenaDbContext _context;

        public ProductController(
            IGenericRepository<Product> productRepository,
            GlutenaDbContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }
        //Burada yaptığımız şey:

        //Dependency Injection ile repository’yi controller’a bağladık.

        //Artık database’den veri çekebiliriz.
        public async Task<IActionResult> Index(string search, int? brandId, string sort, int? categoryId)
        {
            var products = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.ProductCategories)
                .AsQueryable();

            // Arama filtresi
            if (!string.IsNullOrEmpty(search))
                products = products.Where(p => p.ProductName.Contains(search));

            // Marka filtresi
            if (brandId.HasValue)
                products = products.Where(p => p.BrandId == brandId);

            // Kategori filtresi
            if (categoryId.HasValue)
                products = products.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId));

            // Sıralama
            products = sort switch
            {
                "asc" => products.OrderBy(p => p.Price),
                "desc" => products.OrderByDescending(p => p.Price),
                _ => products
            };

            ViewBag.Brands = await _context.Brands.ToListAsync();
            ViewBag.Search = search;
            ViewBag.BrandId = brandId;
            ViewBag.Sort = sort;
            ViewBag.CategoryId = categoryId;

            return View(await products.ToListAsync());
        }
        public async Task<IActionResult> Detail(int id)
        {
            var product = await _context.Products
                .Include(p => p.Brand)
             .Include(p => p.Experiences.Where(e => e.IsApproved && e.IsActive))
                .FirstOrDefaultAsync(p => p.Id == id);

            if (product == null)
                return NotFound();

            ViewBag.RelatedProducts = await _context.Products
                .Where(p => p.Id != id)
                .OrderBy(r => Guid.NewGuid())
                .Take(4)
                .ToListAsync();

            return View(product);
        }
    }

}