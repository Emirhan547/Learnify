using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.AccountDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        // 🔹 REGISTER
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            // Eğer zaten giriş yapmışsa, anasayfaya yönlendir
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(AdminRegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _accountService.AdminRegisterAsync(dto);

            if (!result.Succeeded)
            {
                foreach (var error in result.Errors)
                    ModelState.AddModelError(string.Empty, error.Description);
                return View(dto);
            }

            // başarılı kayıt sonrası login sayfasına yönlendir
            return RedirectToAction(nameof(Login));
        }

        // 🔹 LOGIN
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToAction("Index", "Dashboard");

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _accountService.LoginAsync(dto);

            if (!result.Succeeded)
            {
                ModelState.AddModelError(string.Empty, "Geçersiz kullanıcı adı veya şifre.");
                return View(dto);
            }

            return RedirectToAction("Index", "Dashboard");
        }

        // 🔹 LOGOUT
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _accountService.LogoutAsync();
            return RedirectToAction(nameof(Login));
        }
    }
}
