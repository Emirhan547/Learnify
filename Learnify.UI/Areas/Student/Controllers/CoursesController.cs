using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.CourseReviewDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Learnify.UI.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class CoursesController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;
        private readonly ICourseReviewService _courseReviewService;
        private readonly UserManager<AppUser> _userManager;

        public CoursesController(
            IEnrollmentService enrollmentService,
            ICourseService courseService,
            ICourseReviewService courseReviewService,
            UserManager<AppUser> userManager)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _courseReviewService = courseReviewService;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var enrollments = await _enrollmentService.GetAllWithCourseAndStudentAsync();
            var myCourses = enrollments.Where(x => x.StudentId == studentId).ToList();

            return View(myCourses);
        }

        public async Task<IActionResult> MyCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            var enrollments = await _enrollmentService.GetAllWithCourseAndStudentAsync();

            var myCourses = enrollments
                .Where(x => x.StudentId == user.Id)
                .Select(x => new
                {
                    Course = x.Course,
                    CompletedLessons = x.CompletedLessons,
                    TotalLessons = x.TotalLessons
                }).ToList();

            return View(myCourses);
        }

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

            // ✅ Kurs yorumlarını getir
            ViewBag.Reviews = await _courseReviewService.GetCourseReviewsAsync(id);

            return View(course);
        }

        [HttpPost]
        [Authorize(Roles = "Student")]
        public async Task<IActionResult> AddReview(CreateCourseReviewDto dto)
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var isEnrolled = await _enrollmentService.IsStudentEnrolledAsync(dto.CourseId, studentId);
            if (!isEnrolled)
            {
                TempData["Error"] = "Bu kursa kayıtlı değilsiniz, yorum yapamazsınız.";
                return RedirectToAction("Detail", new { id = dto.CourseId });
            }

            await _courseReviewService.AddReviewAsync(studentId, dto);

            TempData["Success"] = "Yorumunuz başarıyla kaydedildi!";
            return RedirectToAction("Detail", new { id = dto.CourseId });
        }
    }
}
