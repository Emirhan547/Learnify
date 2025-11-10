using Learnify.Business.Abstract;
using Learnify.Entity.Concrete;
using Learnify.UI.Models;
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
        private readonly IInstructorService _instructorService;
        private readonly IEnrollmentService _enrollmentService;
        private readonly UserManager<AppUser> _userManager;

        public DashboardController(
            ICourseService courseService,
            ICategoryService categoryService,
            IInstructorService instructorService,
            IEnrollmentService enrollmentService,
            UserManager<AppUser> userManager)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
            _enrollmentService = enrollmentService;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            var categories = await _categoryService.GetAllAsync();
            var instructors = await _instructorService.GetAllAsync();
            var enrollments = await _enrollmentService.GetAllAsync();
            var students = await _userManager.GetUsersInRoleAsync("Student");

            var model = new DashboardViewModel
            {
                TotalCourses = courses.Data?.Count ?? 0,
                TotalCategories = categories.Data?.Count ?? 0,
                TotalInstructors = instructors.Data?.Count ?? 0,
                TotalStudents = students.Count,
                TotalEnrollments = enrollments.Data?.Count ?? 0,
                LatestCourses = courses.Data?.Take(5).ToList(),
                LatestStudents = students
                    .Where(s => s.IsActive)
                    .OrderByDescending(s => s.Id)
                    .Take(5)
                    .ToList()
            };

            return View(model);
        }
    }
}