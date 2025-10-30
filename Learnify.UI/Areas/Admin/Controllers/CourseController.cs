using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.CourseDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseController : Controller
    {
        private readonly ICourseService _courseService;
        private readonly ICategoryService _categoryService;
        private readonly IInstructorService _instructorService;

        public CourseController(
            ICourseService courseService,
            ICategoryService categoryService,
            IInstructorService instructorService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
            _instructorService = instructorService;
        }

        // 🔹 Dropdown verilerini yüklüyoruz
        private async Task LoadDropdownDataAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            var instructors = await _instructorService.GetAllAsync();

            ViewBag.Categories = new SelectList(categories, "Id", "Name");
            ViewBag.Instructors = new SelectList(instructors, "Id", "FullName");
        }

        // 📘 Kurs listesi
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetCoursesWithInstructorAsync();
            return View(courses);
        }

        // ➕ Yeni kurs oluşturma (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            await LoadDropdownDataAsync();
            return View();
        }

        // ➕ Yeni kurs oluşturma (POST)
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            await _courseService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ✏️ Kurs güncelleme (GET)
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var dto = await _courseService.GetForUpdateAsync(id);
            if (dto == null)
                return NotFound();

            await LoadDropdownDataAsync();
            return View(dto);
        }

        // ✏️ Kurs güncelleme (POST)
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadDropdownDataAsync();
                return View(dto);
            }

            await _courseService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // 🗑️ Kurs silme
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
