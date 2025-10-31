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
        [Authorize(Roles = "Student")]
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

            // dersin kursunu çek
            var lesson = await _lessonService.GetByIdAsync(lessonId);

            return RedirectToAction("Watch", new { lessonId });
        }


    }
}
