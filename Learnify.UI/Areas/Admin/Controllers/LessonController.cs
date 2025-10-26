using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.LessonDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            var values = await _lessonService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateLesson()
        {
            var courses = await _courseService.GetAllAsync();
            ViewBag.Courses = courses.Select(x => new { x.CourseID, x.Title }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateLesson(CreateLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                var courses = await _courseService.GetAllAsync();
                ViewBag.Courses = courses.Select(x => new { x.CourseID, x.Title }).ToList();
                return View(dto);
            }

            await _lessonService.AddAsync(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateLesson(int id)
        {
            var value = await _lessonService.GetByIdAsync(id);
            var courses = await _courseService.GetAllAsync();
            ViewBag.Courses = courses.Select(x => new { x.CourseId, x.Title }).ToList();

            // ✅ ResultLessonDto artık CourseID içeriyor
            var updateDto = new UpdateLessonDto
            {
                LessonId = value.LessonID,
                Title = value.Title,
                VideoUrl = value.VideoUrl,
                CourseID = value.CourseID
            };

            return View(updateDto);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateLesson(UpdateLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                var courses = await _courseService.GetAllAsync();
                ViewBag.Courses = courses.Select(x => new { x.CourseID, x.Title }).ToList();
                return View(dto);
            }

            await _lessonService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteLesson(int id)
        {
            await _lessonService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}