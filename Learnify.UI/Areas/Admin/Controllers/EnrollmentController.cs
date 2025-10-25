using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.EnrollmentDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            var values = await _enrollmentService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateEnrollment()
        {
            var courses = await _courseService.GetAllAsync();
            var students = _userManager.Users.Where(u => u.Profession == null || u.Profession == "Student").ToList();

            ViewBag.Courses = courses.Select(x => new { x.CourseID, x.Title }).ToList();
            ViewBag.Students = students.Select(x => new { Id = x.Id, FullName = x.FullName ?? x.UserName }).ToList();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateEnrollment(CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                var courses = await _courseService.GetAllAsync();
                var students = _userManager.Users.Where(u => u.Profession == null || u.Profession == "Student").ToList();

                ViewBag.Courses = courses.Select(x => new { x.CourseID, x.Title }).ToList();
                ViewBag.Students = students.Select(x => new { Id = x.Id, FullName = x.FullName ?? x.UserName }).ToList();

                return View(dto);
            }

            await _enrollmentService.AddAsync(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateEnrollment(int id)
        {
            var value = await _enrollmentService.GetByIdAsync(id);
            var courses = await _courseService.GetAllAsync();
            var students = _userManager.Users.Where(u => u.Profession == null || u.Profession == "Student").ToList();

            ViewBag.Courses = courses.Select(x => new { x.CourseID, x.Title }).ToList();
            ViewBag.Students = students.Select(x => new { Id = x.Id, FullName = x.FullName ?? x.UserName }).ToList();

            // ✅ DTO dönüşümü düzeltildi - ID'ler artık ResultDto'da mevcut
            var updateDto = new UpdateEnrollmentDto
            {
                EnrollmentID = value.EnrollmentID,
                StudentID = value.StudentID,
                CourseID = value.CourseID
            };

            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEnrollment(UpdateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                var courses = await _courseService.GetAllAsync();
                var students = _userManager.Users.Where(u => u.Profession == null || u.Profession == "Student").ToList();

                ViewBag.Courses = courses.Select(x => new { x.CourseID, x.Title }).ToList();
                ViewBag.Students = students.Select(x => new { Id = x.Id, FullName = x.FullName ?? x.UserName }).ToList();

                return View(dto);
            }

            await _enrollmentService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteEnrollment(int id)
        {
            await _enrollmentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}