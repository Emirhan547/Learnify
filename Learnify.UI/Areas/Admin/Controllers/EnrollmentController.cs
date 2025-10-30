using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.EnrollmentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Learnify.Entity.Concrete;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;
        private readonly UserManager<AppUser> _userManager;

        public EnrollmentController(IEnrollmentService enrollmentService, ICourseService courseService, UserManager<AppUser> userManager)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _userManager = userManager;
        }

        private async Task LoadDropdownDataAsync()
        {
            ViewBag.Courses = new SelectList(await _courseService.GetAllAsync(), "Id", "Title");
            ViewBag.Students = new SelectList(_userManager.Users.Where(u => u.Profession == "Student").ToList(), "Id", "FullName");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var enrollments = await _enrollmentService.GetAllAsync();
            return View(enrollments);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            await _enrollmentService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var enrollment = await _enrollmentService.GetByIdAsync(id);
            if (enrollment == null) return NotFound();

            await LoadDropdownDataAsync();
            return View(enrollment);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            await _enrollmentService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _enrollmentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
