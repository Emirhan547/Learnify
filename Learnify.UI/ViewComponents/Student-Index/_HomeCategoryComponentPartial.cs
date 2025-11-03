using Learnify.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.ViewComponents.Student_Index
{
    public class _HomeCategoryComponentPartial : ViewComponent
    {
        private readonly ICategoryService _categoryService;

        public _HomeCategoryComponentPartial(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            // sadece aktif kategoriler ve maksimum 4 tane
            var model = categories
                .Where(x => x.IsActive)
                .Take(4)
                .ToList();

            return View(model);
        }
    }
}