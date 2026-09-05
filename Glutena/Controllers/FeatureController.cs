using Glutena.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Controllers
{
    public class FeatureController : Controller
    {
        private readonly GlutenaDbContext _context;

        public FeatureController(GlutenaDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var features = await _context.Features.ToListAsync();

            return View(features);
        }
    }
}