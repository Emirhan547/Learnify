using Learnify.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class StudentController : Controller
    {
        private readonly IStudentService _studentService;

        public StudentController(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task<IActionResult> Index()
        {
            var students = await _studentService.GetAllAsync();
            return View(students);
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var courses = await _studentService.GetStudentCoursesAsync(id);
            return PartialView("_StudentCoursesPartial", courses); // ✅ PartialView olarak döner
        }


        [HttpGet]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            await _studentService.DeleteAsync(id);
            TempData["Success"] = "Öğrenci başarıyla silindi.";
            return RedirectToAction(nameof(Index));
        }
    }
}
