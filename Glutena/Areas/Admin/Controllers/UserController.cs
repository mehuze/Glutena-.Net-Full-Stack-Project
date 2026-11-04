using Glutena.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Glutena.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly GlutenaDbContext _context;

        public UserController(
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager,
            GlutenaDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _context = context;
        }

        // Tüm kullanıcıları listele
        public async Task<IActionResult> Index()
        {
            var users = _userManager.Users.ToList();
            var userRoles = new Dictionary<string, IList<string>>();

            foreach (var user in users)
            {
                var roles = await _userManager.GetRolesAsync(user);
                userRoles[user.Id] = roles;
            }

            ViewBag.UserRoles = userRoles;
            ViewBag.CurrentUserId = _userManager.GetUserId(User);
            return View(users);
        }

        // Kullanıcı sil
        public async Task<IActionResult> Delete(string id)
        {
            if (IsCurrentUser(id))
            {
                TempData["Error"] = "Kendi hesabınızı silemezsiniz.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                // Kullanıcının yorumlarını sahipsiz yap (yorumlar sitede kalır)
                var experiences = await _context.Experiences
                    .Where(e => e.UserId == id)
                    .ToListAsync();
                foreach (var exp in experiences)
                    exp.UserId = null;
                await _context.SaveChangesAsync();

                await _userManager.DeleteAsync(user);
            }
            return RedirectToAction("Index");
        }

        // Admin rolü ver
        public async Task<IActionResult> MakeAdmin(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                if (!await _roleManager.RoleExistsAsync("Admin"))
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));

                await _userManager.AddToRoleAsync(user, "Admin");
            }
            return RedirectToAction("Index");
        }

        // Admin rolünü kaldır
        public async Task<IActionResult> RemoveAdmin(string id)
        {
            if (IsCurrentUser(id))
            {
                TempData["Error"] = "Kendi admin yetkinizi kaldıramazsınız.";
                return RedirectToAction("Index");
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, "Admin");
            }
            return RedirectToAction("Index");
        }

        // Bu işlem, giriş yapmış kişinin kendi hesabına mı yapılıyor?
        private bool IsCurrentUser(string id)
        {
            return id == _userManager.GetUserId(User);
        }
    }
}