using Learnify.Business.Abstract;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly ILessonService _lessonService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(ICourseService courseService, ICategoryService categoryService, IEnrollmentService enrollmentService, ILessonService lessonService, UserManager<AppUser> userManager)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _enrollmentService = enrollmentService;
            _lessonService = lessonService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var enrollments = await _enrollmentService.GetAllAsync();
            var lessons = await _lessonService.GetAllAsync();

            var students = _userManager.Users.Count(u => u.Profession == "Student");
            var instructors = _userManager.Users.Count(u => u.Profession != "Student");

            ViewBag.TotalCourses = courses.Count;
            ViewBag.TotalCategories = categories.Count;
            ViewBag.TotalEnrollments = enrollments.Count;
            ViewBag.TotalLessons = lessons.Count;
            ViewBag.TotalStudents = students;
            ViewBag.TotalInstructors = instructors;
            ViewBag.RecentEnrollments = enrollments.OrderByDescending(x => x.EnrolledDate).Take(5).ToList();

            return View();
        }
    }
}
