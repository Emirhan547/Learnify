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

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _enrollmentService.GetAllWithCourseAndStudentAsync();

            var myCourses = result.Data?
                .Where(x => x.StudentId == studentId)
                .ToList() ?? new List<Learnify.DTO.DTOs.EnrollmentDto.ResultEnrollmentDto>();

            return View(myCourses);
        }

        [HttpGet]
        public async Task<IActionResult> MyCourses()
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _enrollmentService.GetAllWithCourseAndStudentAsync();

            var myCourses = result.Data?
                .Where(x => x.StudentId == user.Id)
                .Select(x => new
                {
                    x.Course,
                    x.CompletedLessons,
                    x.TotalLessons
                })
                .ToList();

            return View(myCourses);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var courseResult = await _courseService.GetByIdAsync(id);
            if (!courseResult.Success || courseResult.Data == null)
                return RedirectToAction(nameof(Index));

            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var isEnrolled = await _enrollmentService.IsStudentEnrolledAsync(id, studentId);

            var reviewResult = await _courseReviewService.GetAllAsync();
            var courseReviews = reviewResult.Data?
                .Where(r => r.CourseId == id)
                .ToList() ?? new List<ResultCourseReviewDto>();

            ViewBag.IsEnrolled = isEnrolled;
            ViewBag.Reviews = courseReviews;

            return View(courseResult.Data);
        }

        [HttpPost]
        public async Task<IActionResult> AddReview(CreateCourseReviewDto dto)
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var isEnrolled = await _enrollmentService.IsStudentEnrolledAsync(dto.CourseId, studentId);
            if (isEnrolled == null || !isEnrolled.Success)
                return Forbid("Bu kursa kayıtlı değilsiniz, yorum yapamazsınız.");

            await _courseReviewService.AddAsync(dto);
            return RedirectToAction(nameof(Detail), new { id = dto.CourseId });
        }

    }
}
