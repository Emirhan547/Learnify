using Learnify.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Learnify.UI.ViewComponents.Student_Index
{
    public class _HomeCourseComponentPartial : ViewComponent
    {
        private readonly ICourseService _courseService;

        public _HomeCourseComponentPartial(ICourseService courseService)
        {
            _courseService = courseService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var courses = await _courseService.GetAllAsync();

            // 🧠 Sadece aktif ve yayında olan kursları göster
            var activeCourses = courses.Where(x => x.IsActive).Take(6).ToList();

            return View(activeCourses);
        }
    }
}