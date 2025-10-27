using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.CategoryDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // 📋 Kategori Listesi
        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            return View(categories);
        }

        // ➕ Yeni Kategori Formu
        [HttpGet]
        public IActionResult CreateCategory() => View();

        // ✅ Yeni Kategori Kaydet
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCategory(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.AddAsync(dto);
            TempData["Success"] = "Kategori başarıyla eklendi.";
            return RedirectToAction(nameof(Index));
        }

        // ✏️ Güncelleme Formu
        [HttpGet]
        public async Task<IActionResult> UpdateCategory(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);
            if (category == null)
                return NotFound();

            return View(category);
        }

        // ✅ Güncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateCategory(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.UpdateAsync(dto);
            TempData["Success"] = "Kategori başarıyla güncellendi.";
            return RedirectToAction(nameof(Index));
        }

        // ❌ Sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteAsync(id);
            TempData["Success"] = "Kategori silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
