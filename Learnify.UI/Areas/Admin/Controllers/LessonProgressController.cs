using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.LessonProgressDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LessonProgressController : Controller
    {
        private readonly ILessonProgressService _lessonProgressService;
        private readonly ICourseService _courseService;
        private readonly ILessonService _lessonService;

        public LessonProgressController(
            ILessonProgressService lessonProgressService,
            ICourseService courseService,
            ILessonService lessonService)
        {
            _lessonProgressService = lessonProgressService;
            _courseService = courseService;
            _lessonService = lessonService;
        }

        // 📊 Belirli bir öğrencinin kurs ilerlemesi
        [HttpGet]
        public async Task<IActionResult> Index(int courseId, int studentId)
        {
            var completed = await _lessonProgressService.GetCompletedCountAsync(courseId, studentId);
            var lessons = await _lessonService.GetLessonsByCourseIdAsync(courseId);

            ViewData["CourseId"] = courseId;
            ViewData["StudentId"] = studentId;
            ViewData["CompletedCount"] = completed.Data;
            ViewData["TotalLessons"] = lessons.Data?.Count ?? 0;

            return View(lessons.Data);
        }

        // ✅ Dersi tamamla veya ilerlemeyi güncelle
        [HttpPost]
        public async Task<IActionResult> UpdateProgress(CreateLessonProgressDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Veri hatalı.");

            await _lessonProgressService.AddOrUpdateAsync(dto);
            return RedirectToAction(nameof(Index), new { courseId = dto.CourseId, studentId = dto.StudentId });
        }

        // 🔎 Belirli dersin tamamlanma durumu (AJAX endpoint)
        [HttpGet]
        public async Task<IActionResult> IsCompleted(int lessonId, int studentId)
        {
            var result = await _lessonProgressService.IsLessonCompletedAsync(lessonId, studentId);
            return Json(new { completed = result.Data });
        }
    }
}
