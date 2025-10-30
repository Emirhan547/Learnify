using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.LessonDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        private async Task LoadDropdownDataAsync()
        {
            ViewBag.Courses = new SelectList(await _courseService.GetAllAsync(), "Id", "Title");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var lessons = await _lessonService.GetAllAsync();
            return View(lessons);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            await _lessonService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var dto = await _lessonService.GetForUpdateAsync(id);
            if (dto == null)
                return NotFound();

            await LoadDropdownDataAsync();
            return View(dto);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            await _lessonService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _lessonService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
