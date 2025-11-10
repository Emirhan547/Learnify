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

        // 📄 Tüm kursları listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _courseService.GetCoursesWithInstructorAsync();
            return View(result.Data);
        }

        // ➕ Kurs ekleme sayfası
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // DropDown verileri (sadece DTO listesi döner)
            ViewBag.Categories = (await _categoryService.GetAllAsync()).Data;
            ViewBag.Instructors = (await _instructorService.GetAllAsync()).Data;

            return View();
        }

        // ✅ Kurs ekleme işlemi
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = (await _categoryService.GetAllAsync()).Data;
                ViewBag.Instructors = (await _instructorService.GetAllAsync()).Data;
                return View(dto);
            }

            await _courseService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ✏️ Kurs güncelleme sayfası
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _courseService.GetForUpdateAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            ViewBag.Categories = (await _categoryService.GetAllAsync()).Data;
            ViewBag.Instructors = (await _instructorService.GetAllAsync()).Data;

            return View(result.Data);
        }

        // ✅ Kurs güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCourseDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Categories = (await _categoryService.GetAllAsync()).Data;
                ViewBag.Instructors = (await _instructorService.GetAllAsync()).Data;
                return View(dto);
            }

            await _courseService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ❌ Kurs silme işlemi
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // 📘 Kurs detay sayfası (isteğe bağlı)
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _courseService.GetCourseDetailsAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }
    }
}
