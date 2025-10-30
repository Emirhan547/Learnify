using Learnify.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Learnify.UI.Controllers
{
    [Authorize(Roles = "Student")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly IEnrollmentService _enrollmentService;

        public LessonController(ILessonService lessonService, IEnrollmentService enrollmentService)
        {
            _lessonService = lessonService;
            _enrollmentService = enrollmentService;
        }

        public async Task<IActionResult> Index(int courseId)
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Kursa kayıtlı mı kontrol
            var isEnrolled = await _enrollmentService.IsStudentEnrolledAsync(courseId, studentId);
            if (!isEnrolled)
            {
                TempData["Error"] = "Bu kursa erişiminiz yok!";
                return RedirectToAction("Index", "Course");
            }

            var lessons = await _lessonService.GetLessonsByCourseIdAsync(courseId);
            ViewBag.CourseId = courseId;

            return View(lessons);
        }
        public async Task<IActionResult> Watch(int lessonId)
        {
            var lesson = await _lessonService.GetByIdAsync(lessonId);
            if (lesson == null) return RedirectToAction("Index", "Home");

            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // öğrenci bu derse ait kursa kayıtlı mı?
            var isEnrolled = await _enrollmentService.IsStudentEnrolledAsync(lesson.CourseId, studentId);
            if (!isEnrolled)
            {
                TempData["Error"] = "Bu derse erişim izniniz yok!";
                return RedirectToAction("Index", "Course");
            }

            // Tüm dersler
            var lessons = await _lessonService.GetLessonsByCourseIdAsync(lesson.CourseId);
            ViewBag.Lessons = lessons;

            return View(lesson);
        }

    }
}
