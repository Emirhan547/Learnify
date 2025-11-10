using Learnify.Business.Abstract;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Learnify.UI.Controllers
{
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly UserManager<AppUser> _userManager;

        public CourseController(ICourseService courseService, IEnrollmentService enrollmentService, UserManager<AppUser> userManager)
        {
            _courseService = courseService;
            _enrollmentService = enrollmentService;
            _userManager = userManager;
        }

        // 🔹 Tüm kurslar
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _courseService.GetAllAsync();
            if (result?.Success != true || result.Data == null)
                return View(new List<Entity.Concrete.Course>());

            var activeCourses = result.Data.Where(x => x.IsActive).ToList();
            return View(activeCourses);
        }

        // 🔹 Kurs Detay
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _courseService.GetByIdAsync(id);
            if (result?.Success != true || result.Data == null)
                return RedirectToAction(nameof(Index));

            bool isEnrolled = false;

            if (User.Identity?.IsAuthenticated == true)
            {
                var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                var enrollResult = await _enrollmentService.IsStudentEnrolledAsync(id, studentId);
                isEnrolled = enrollResult != null && enrollResult.Success;
            }

            var model = result.Data;
            model.IsUserEnrolled = isEnrolled;

            return View(model);
        }

        // 🔹 Kursa Katıl (POST)
        [Authorize(Roles = "Student")]
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var enrollResult = await _enrollmentService.EnrollStudentAsync(courseId, studentId);

            TempData[enrollResult.Success ? "Success" : "Error"] = enrollResult.Message;
            return RedirectToAction(nameof(Detail), new { id = courseId });
        }

        // 🔹 Öğrencinin kurs listesi
        [Authorize(Roles = "Student")]
        [HttpGet]
        public async Task<IActionResult> MyCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _enrollmentService.GetAllWithCourseAndStudentAsync();

            var myCourses = result.Data?
                .Where(x => x.StudentId == user.Id)
                .Select(x => x.Course)
                .ToList() ?? new List<Course>();

            return View(myCourses);
        }
    }
}
