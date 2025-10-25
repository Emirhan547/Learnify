using Learnify.DTO.DTOs.AccountDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Sadece Admin yeni kullanıcı ekleyebilir
    public class RegisterController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        public RegisterController(
            UserManager<AppUser> userManager,
            RoleManager<IdentityRole<int>> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // GET: Admin/Register/Index
        [HttpGet]
        public IActionResult Index()
        {
            // Rolleri ViewBag'e ekle
            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View();
        }

        // POST: Admin/Register/Index
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Index(AdminRegisterDto model)
        {
            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Email kontrolü
            var existingUser = await _userManager.FindByEmailAsync(model.Email);
            if (existingUser != null)
            {
                ModelState.AddModelError("Email", "Bu email adresi zaten kullanılıyor");
                return View(model);
            }

            // Username kontrolü
            var existingUsername = await _userManager.FindByNameAsync(model.UserName);
            if (existingUsername != null)
            {
                ModelState.AddModelError("UserName", "Bu kullanıcı adı zaten kullanılıyor");
                return View(model);
            }

            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                FullName = model.FullName,
                Profession = model.Profession,
                EmailConfirmed = true // Admin tarafından eklenenler onaylı
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                // Rol ataması
                if (!string.IsNullOrEmpty(model.Role))
                {
                    await _userManager.AddToRoleAsync(user, model.Role);
                }

                TempData["SuccessMessage"] = $"Kullanıcı başarıyla oluşturuldu. Rol: {model.Role}";
                return RedirectToAction("Index", "UserManagement"); // Kullanıcı listesi sayfası (ileride yapılacak)
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        // GET: Admin/Register/CreateQuick (Hızlı kayıt - modal için)
        [HttpGet]
        public IActionResult CreateQuick()
        {
            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return PartialView("_CreateQuickPartial");
        }
    }
}