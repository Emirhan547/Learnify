using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.CourseDto;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CourseController (ICourseService _courseService) : Controller
    {

        public async Task<IActionResult> Index()
        {
            var values=await _courseService.GetAllAsync();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateCourse()
        {
            return View();
        }
        [HttpPost]
        public async Task <IActionResult> CreateCourse(CreateCourseDto dto)
        {
            await _courseService.AddAsync(dto);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> UpdateCourse(int id)
        {
            var value=await _courseService.GetByIdAsync(id);
            return View(value);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCourse(UpdateCourseDto dto)
        {
            await _courseService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

    }
}
