using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.LessonDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly ICourseService _courseService;

        public LessonController(ILessonService lessonService, ICourseService courseService)
        {
            _lessonService = lessonService;
            _courseService = courseService;
        }

        private async Task LoadCoursesAsync()
        {
            var courses = await _courseService.GetAllAsync();
            ViewBag.Courses = courses.Select(c => new SelectListItem
            {
                Text = c.Title,
                Value = c.Id.ToString()
            }).ToList();
        }

        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonService.GetAllAsync();
            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLesson()
        {
            await LoadCoursesAsync();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateLesson(CreateLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCoursesAsync();
                TempData["Error"] = "Lütfen tüm alanları eksiksiz doldurun.";
                return View(dto);
            }

            await _lessonService.AddAsync(dto);
            TempData["Success"] = "Ders başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateLesson(int id)
        {
            var lesson = await _lessonService.GetByIdAsync(id);
            if (lesson == null)
                return NotFound();

            await LoadCoursesAsync();
            var dto = new UpdateLessonDto
            {
                Id = lesson.Id,
                Title = lesson.Title,
                VideoUrl = lesson.VideoUrl,
                CourseId = lesson.CourseId
            };
            return View(dto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateLesson(UpdateLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCoursesAsync();
                TempData["Error"] = "Lütfen geçerli bilgileri giriniz.";
                return View(dto);
            }

            await _lessonService.UpdateAsync(dto);
            TempData["Success"] = "Ders bilgileri güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteLesson(int id)
        {
            await _lessonService.DeleteAsync(id);
            TempData["Success"] = "Ders başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
