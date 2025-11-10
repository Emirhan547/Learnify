using Learnify.DTO.DTOs.AccountDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [AllowAnonymous]
    public class AccountController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public AccountController(
            SignInManager<AppUser> signInManager,
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // 🔑 Giriş sayfası
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // 🔓 Giriş işlemi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(dto);
            }

            // Kullanıcının Admin rolünde olup olmadığını kontrol et
            if (!await _userManager.IsInRoleAsync(user, "Admin"))
            {
                ModelState.AddModelError("", "Yalnızca yöneticiler giriş yapabilir.");
                return View(dto);
            }

            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, dto.RememberMe, false);

            if (result.Succeeded)
                return RedirectToAction("Index", "Dashboard", new { area = "Admin" });

            ModelState.AddModelError("", "Geçersiz e-posta veya şifre.");
            return View(dto);
        }

        // 🚪 Çıkış işlemi
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account", new { area = "Admin" });
        }
    }
}
