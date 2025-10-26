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

        public CourseController(ICourseService courseService, ICategoryService categoryService)
        {
            _courseService = courseService;
            _categoryService = categoryService;
        }

        // 📌 Yardımcı metot — Tekrarlayan kategori yüklemelerini ortadan kaldırır.
        private async Task LoadCategoriesAsync()
        {
            var categories = await _categoryService.GetAllAsync();
            ViewBag.Categories = categories
                .Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })
                .ToList();
        }

        // 📋 Tüm kursları listele
        public async Task<IActionResult> Index()
        {
            var courses = await _courseService.GetAllAsync();
            return View(courses);
        }

        // ➕ Yeni kurs sayfası
        [HttpGet]
        public async Task<IActionResult> CreateCourse()
        {
            await LoadCategoriesAsync();
            return View();
        }

        // ✅ Yeni kurs oluştur
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCourse(CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return View(dto);
            }

            try
            {
                await _courseService.AddAsync(dto);
                TempData["Success"] = "Kurs başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                await LoadCategoriesAsync();
                return View(dto);
            }
        }

        // ✏️ Kurs güncelleme sayfası
        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            var course = await _courseService.GetByIdAsync(id);
            if (course == null)
                return NotFound();

            await LoadCategoriesAsync();
            return View(course);
        }

        // ✅ Kurs güncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                await LoadCategoriesAsync();
                return View(dto);
            }

            try
            {
                await _courseService.UpdateAsync(dto);
                TempData["Success"] = "Kurs başarıyla güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                await LoadCategoriesAsync();
                return View(dto);
            }
        }

        // ❌ Kurs sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            try
            {
                await _courseService.DeleteAsync(id);
                TempData["Success"] = "Kurs silindi.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
