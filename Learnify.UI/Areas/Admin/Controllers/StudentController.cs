
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public StudentController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        // 📋 Tüm öğrencileri listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var students = await _userManager.Users
                .Where(u => u.IsActive && u.Profession == "Student")
                .ToListAsync();

            return View(students);
        }

        // 🔍 Öğrenci detay sayfası
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var student = await _userManager.FindByIdAsync(id.ToString());
            if (student == null || student.Profession != "Student")
                return NotFound();

            return View(student);
        }

        // ❌ Öğrenciyi pasife al
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Deactivate(int id)
        {
            var student = await _userManager.FindByIdAsync(id.ToString());
            if (student == null) return NotFound();

            student.IsActive = false;
            await _userManager.UpdateAsync(student);

            return RedirectToAction(nameof(Index));
        }

        // 🔄 Öğrenciyi tekrar aktifleştir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Activate(int id)
        {
            var student = await _userManager.FindByIdAsync(id.ToString());
            if (student == null) return NotFound();

            student.IsActive = true;
            await _userManager.UpdateAsync(student);

            return RedirectToAction(nameof(Index));
        }

        // 🧾 Pasif öğrenciler listesi
        [HttpGet]
        public async Task<IActionResult> PassiveList()
        {
            var students = await _userManager.Users
                .Where(u => !u.IsActive && u.Profession == "Student")
                .ToListAsync();

            return View("Index", students);
        }
    }
}
