using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.CourseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {

        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;

        public CourseController(ICourseService courseService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var values = await _courseService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories.Select(x => new { x.CategoryID, x.CategoryName }).ToList();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                ViewBag.Categories = categories.Select(x => new { x.CategoryID, x.CategoryName }).ToList();
                return View(dto);
            }

            await _courseService.AddAsync(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            var value = await _courseService.GetByIdAsync(id);
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories.Select(x => new { x.CategoryID, x.CategoryName }).ToList();
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync();
                ViewBag.Categories = categories.Select(x => new { x.CategoryID, x.CategoryName }).ToList();
                return View(dto);
            }

            await _courseService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteCourse(int id)
        {
            await _courseService.DeleteAsync(id);
            return RedirectToAction("Index");
        }

    }
}
