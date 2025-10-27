using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.CourseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;

        public CourseController(ICourseService courseService, ICategoryService categoryService, IInstructorService instructorService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
        }

        private async Task LoadDropdownDataAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            var instructors = await _instructorService.GetAllAsync();

            ViewBag.Categories = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            ViewBag.Instructors = instructors.Select(x => new SelectListItem
            {
                Text = x.FullName ?? x.UserName,
                Value = x.Id.ToString()
            }).ToList();
        }

        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            return View(courses);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            await _courseService.AddAsync(dto);
            TempData["Success"] = "Kurs başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            await LoadDropdownDataAsync();
            return View(course);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            await _courseService.UpdateAsync(dto);
            TempData["Success"] = "Kurs başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteAsync(id);
            TempData["Success"] = "Kurs başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
