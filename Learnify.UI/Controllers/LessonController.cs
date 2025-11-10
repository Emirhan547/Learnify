using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.LessonProgressDto;
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
        private readonly ILessonProgressService _lessonProgressService;

        public LessonController(ILessonService lessonService, IEnrollmentService enrollmentService, ILessonProgressService lessonProgressService)
        {
            _lessonService = lessonService;
            _enrollmentService = enrollmentService;
            _lessonProgressService = lessonProgressService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int courseId)
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            var enrollCheck = await _enrollmentService.IsStudentEnrolledAsync(courseId, studentId);
            if (enrollCheck?.Success != true)
            {
                TempData["Error"] = "Bu kursa erişiminiz yok!";
                return RedirectToAction("Index", "Course");
            }

            var lessons = await _lessonService.GetLessonsByCourseIdAsync(courseId);
            return View(lessons.Data ?? new List<object>());
        }

        [HttpGet]
        public async Task<IActionResult> Watch(int lessonId)
        {
            var lesson = await _lessonService.GetByIdAsync(lessonId);
            if (lesson?.Success != true || lesson.Data == null)
                return RedirectToAction("Index", "Home");

            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var enrollCheck = await _enrollmentService.IsStudentEnrolledAsync(lesson.Data.CourseId, studentId);

            if (enrollCheck?.Success != true)
            {
                TempData["Error"] = "Bu derse erişim izniniz yok!";
                return RedirectToAction("Index", "Course");
            }

            var lessons = await _lessonService.GetLessonsByCourseIdAsync(lesson.Data.CourseId);
            ViewBag.Lessons = lessons.Data;

            return View(lesson.Data);
        }

        [HttpPost]
        public async Task<IActionResult> CompleteLesson(int lessonId)
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            await _lessonProgressService.AddOrUpdateAsync(new CreateLessonProgressDto
            {
                LessonId = lessonId,
                StudentId = studentId,
                IsCompleted = true
            });

            TempData["Success"] = "Dersi tamamladınız 🎉";

            return RedirectToAction("Watch", new { lessonId });
        }
    }
}
