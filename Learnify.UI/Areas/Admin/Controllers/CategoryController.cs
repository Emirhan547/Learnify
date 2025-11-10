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

        // Listeleme
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _categoryService.GetAllAsync();
            return View(result.Data);
        }

        // Yeni kategori formu
        [HttpGet]
        public IActionResult Create() => View();

        // Ekleme
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // Güncelleme formu
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _categoryService.GetForUpdateAsync(id);
            return View(result.Data);
        }

        // Güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCategoryDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _categoryService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // Silme
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _categoryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
