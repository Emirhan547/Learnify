using Learnify.DTO.DTOs.AccountDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class ProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public ProfileController(UserManager<AppUser> userManager, IWebHostEnvironment env)
        {
            _userManager = userManager;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

            return View(new UpdateStudentProfileDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email!,
                ExistingImage = user.ProfileImage
            });
        }

        [HttpPost]
        public async Task<IActionResult> Index(UpdateStudentProfileDto model)
        {
            var user = await _userManager.FindByIdAsync(model.Id.ToString());
            if (user == null)
                return View(model);

            if (model.ProfileImage != null)
            {
                var folder = Path.Combine(_env.WebRootPath, "studentprofiles");
                Directory.CreateDirectory(folder);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.ProfileImage.FileName);
                var filePath = Path.Combine(folder, fileName);

                await using var stream = new FileStream(filePath, FileMode.Create);
                await model.ProfileImage.CopyToAsync(stream);

                user.ProfileImage = "/studentprofiles/" + fileName;
            }

            user.FullName = model.FullName;

            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Profil güncellenemedi!");
                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult ChangePassword() => View(new ChangePasswordDto());

        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
                return View(model);

            var user = await _userManager.FindByIdAsync(model.UserId.ToString());
            if (user == null)
                return View(model);

            var result = await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if (!result.Succeeded)
            {
                foreach (var err in result.Errors)
                    ModelState.AddModelError("", err.Description);

                return View(model);
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
