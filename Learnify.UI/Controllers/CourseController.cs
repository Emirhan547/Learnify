using Learnify.Business.Abstract;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Linq;

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
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            var activeCourses = courses.Where(x => x.IsActive).ToList();
            return View(activeCourses);
        }

        // 🔹 Kurs Detay
        public async Task<IActionResult> Detail(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null) return RedirectToAction("Index");

            ViewBag.IsEnrolled = false;

            if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
                ViewBag.IsEnrolled = await _enrollmentService.IsStudentEnrolledAsync(id, studentId);
            }

            return View(course);
        }

        // 🔹 Kursa Katıl (POST)
        [Authorize(Roles = "Student")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(int courseId)
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var success = await _enrollmentService.EnrollStudentAsync(courseId, studentId);

            if (!success)
                TempData["Error"] = "Bu kursa zaten kayıt oldunuz.";
            else
                TempData["Success"] = "Kursa başarıyla kaydoldunuz!";

            return RedirectToAction("Detail", new { id = courseId });
        }

        // 🔹 Öğrencinin kurs listesi
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> MyCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            var enrollments = await _enrollmentService.GetAllWithCourseAndStudentAsync();

            var myCourses = enrollments
                .Where(x => x.StudentId == user.Id)
                .Select(x => x.Course)
                .ToList();

            return View(myCourses);
        }
    }
}
