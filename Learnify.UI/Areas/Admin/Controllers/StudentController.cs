using Learnify.Business.Abstract;
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
        private readonly IEnrollmentService _enrollmentService;

        public StudentController(UserManager<AppUser> userManager, IEnrollmentService enrollmentService)
        {
            _userManager = userManager;
            _enrollmentService = enrollmentService;
        }

        [HttpGet]
        public IActionResult Index(string? search, string? status)
        {
            var query = _userManager.Users.Where(u => u.Profession == "Student");

            if (!string.IsNullOrEmpty(search))
                query = query.Where(u => u.FullName.Contains(search) || u.Email.Contains(search));

            if (status == "active")
                query = query.Where(u => u.IsActive);
            else if (status == "passive")
                query = query.Where(u => !u.IsActive);

            var students = query.ToList();

            ViewBag.Search = search;
            ViewBag.Status = status;

            return View(students);
        }

        [HttpGet]
        public async Task<IActionResult> GetStudentDetail(int id)
        {
            var student = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (student == null) return NotFound();

            var enrollments = await _enrollmentService.GetAllAsync();
            var enrolledCourses = enrollments
                .Where(e => e.StudentId == id)
                .Select(e => e.CourseTitle)
                .ToList();

            return PartialView("_StudentDetailPartial", new
            {
                Student = student,
                Courses = enrolledCourses
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ToggleStatus(int id)
        {
            var student = await _userManager.FindByIdAsync(id.ToString());
            if (student == null) return NotFound();

            student.IsActive = !student.IsActive;
            await _userManager.UpdateAsync(student);

            return RedirectToAction(nameof(Index));
        }
    }
}
