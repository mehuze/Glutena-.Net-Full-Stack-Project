using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Glutena.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: /Account/Login
        public IActionResult Login()
        {
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        public async Task<IActionResult> Login(string email, string password, bool rememberMe)
        {
            var result = await _signInManager.PasswordSignInAsync(email, password, rememberMe, false);

            if (result.Succeeded)
            {
                var user = await _userManager.FindByEmailAsync(email);
                var isAdmin = user != null && await _userManager.IsInRoleAsync(user, "Admin");

                if (isAdmin)
                    return RedirectToAction("Index", "Dashboard", new { area = "Admin" });
                else
                    return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = "E-posta veya şifre hatalı!";
            return View();
        }

        // GET: /Account/Register
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        public async Task<IActionResult> Register(string email, string password, string confirmPassword, bool kvkkRead = false)
        {
            // KVKK Aydınlatma Metni okundu mu?
            if (!kvkkRead)
            {
                ViewBag.Error = "Kayıt olmak için KVKK Aydınlatma Metni'ni okuduğunuzu işaretlemelisiniz.";
                return View();
            }

            if (password != confirmPassword)
            {
                ViewBag.Error = "Şifreler eşleşmiyor!";
                return View();
            }

            var user = new IdentityUser { UserName = email, Email = email };
            var result = await _userManager.CreateAsync(user, password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("User"))
                    await _roleManager.CreateAsync(new IdentityRole("User"));

                await _userManager.AddToRoleAsync(user, "User");
                await _signInManager.SignInAsync(user, false);
                TempData["Welcome"] = "Hoş geldin! Hesabın başarıyla oluşturuldu.";
                return RedirectToAction("Index", "Home");
            }

            ViewBag.Error = string.Join(", ", result.Errors.Select(e => e.Description));
            return View();
        }

        // Çıkış
        [HttpPost]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}