using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;
        private readonly UserManager<AppUser> _userManager;

        public EnrollmentController(
            IEnrollmentService enrollmentService,
            ICourseService courseService,
            UserManager<AppUser> userManager)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _userManager = userManager;
        }

        // 🔹 Dropdown verilerini yükle
        private async Task LoadDropdownDataAsync()
        {
            var courses = await _courseService.GetAllAsync();
            var students = _userManager.Users
                .Where(u => u.Profession == null || u.Profession == "Student")
                .ToList();

            ViewBag.Courses = courses
                .Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString() // ✅ CourseID yerine Id
                })
                .ToList();

            ViewBag.Students = students
                .Select(s => new SelectListItem
                {
                    Text = s.FullName ?? s.UserName ?? "Bilinmiyor",
                    Value = s.Id.ToString()
                })
                .ToList();
        }

        // 📋 Listeleme
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllAsync();
            return View(enrollments);
        }

        // ➕ Yeni kayıt formu
        [HttpGet]
        public async Task<IActionResult> CreateEnrollment()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        // ✅ Yeni kayıt oluştur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateEnrollment(CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            var result = await _enrollmentService.AddAsync(dto);
            if (result)
                return RedirectToAction(nameof(Index));

            await LoadDropdownDataAsync();
            return View(dto);
        }

        // ✏️ Güncelleme formu
        [HttpGet]
        public async Task<IActionResult> UpdateEnrollment(int id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null)
                return NotFound();

            await LoadDropdownDataAsync();

            var dto = new UpdateEnrollmentDto
            {
                Id = enrollment.Id,          // ✅ EnrollmentID yerine Id
                StudentId = enrollment.StudentId, // ✅ StudentID yerine StudentId
                CourseId = enrollment.CourseId    // ✅ CourseID yerine CourseId
            };

            return View(dto);
        }

        // ✅ Güncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateEnrollment(UpdateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            var result = await _enrollmentService.UpdateAsync(dto);
            if (result)
                return RedirectToAction(nameof(Index));

            await LoadDropdownDataAsync();
            return View(dto);
        }

        // ❌ Sil
        [HttpGet]
        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            await _enrollmentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
