using AutoMapper;
using Learnify.DTO.DTOs.AccountDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IMapper _mapper;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _mapper = mapper;
        }

        // 🧩 REGISTER
        [HttpGet, AllowAnonymous]
        public IActionResult Register() =>
            User.Identity?.IsAuthenticated == true
                ? RedirectToAction("Index", "Dashboard")
                : View();

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = _mapper.Map<AppUser>(dto);
            var result = await _userManager.CreateAsync(user, dto.Password);

            if (!result.Succeeded)
                return ViewWithErrors(result, dto);

            await _userManager.AddToRoleAsync(user, "Admin");
            return RedirectToAction(nameof(Login));
        }

        // 🔐 LOGIN
        [HttpGet, AllowAnonymous]
        public IActionResult Login() =>
            User.Identity?.IsAuthenticated == true
                ? RedirectToAction("Index", "Dashboard")
                : View();

        [HttpPost, AllowAnonymous, ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var result = await _signInManager.PasswordSignInAsync(dto.Email, dto.Password, dto.RememberMe, false);

            if (!result.Succeeded)
                return ViewWithMessage("Geçersiz e-posta veya şifre.", dto);

            return RedirectToAction("Index", "Dashboard");
        }

        // 🚪 LOGOUT
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(Login));
        }

        // 🔑 CHANGE PASSWORD
        [Authorize, HttpGet]
        public IActionResult ChangePassword() => View();

        [Authorize, HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var user = await _userManager.GetUserAsync(User);
            if (user == null)
                return RedirectToAction(nameof(Login));

            var result = await _userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);
            if (!result.Succeeded)
                return ViewWithErrors(result, dto);

            await _signInManager.RefreshSignInAsync(user);
            ViewBag.Success = "Şifre başarıyla güncellendi.";
            return View();
        }

        // 🧱 Ortak Hata Yönetimi Yardımcıları
        private ViewResult ViewWithErrors(IdentityResult result, object model)
        {
            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);
            return View(model);
        }

        private ViewResult ViewWithMessage(string message, object model)
        {
            ModelState.AddModelError("", message);
            return View(model);
        }
    }
}
