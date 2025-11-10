using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.LessonDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class LessonController : Controller
    {
        private readonly ILessonService _lessonService;
        private readonly ICourseService _courseService;

        public LessonController(ILessonService lessonService, ICourseService courseService)
        {
            _lessonService = lessonService;
            _courseService = courseService;
        }

        // 📘 Dersleri listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _lessonService.GetAllAsync();
            return View(result.Data);
        }

        // ➕ Yeni ders oluşturma sayfası
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await _courseService.GetAllAsync();
            ViewBag.Courses = courses.Data;
            return View();
        }

        // ✅ Yeni ders ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = (await _courseService.GetAllAsync()).Data;
                return View(dto);
            }

            await _lessonService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ✏️ Güncelleme formu
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _lessonService.GetForUpdateAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            ViewBag.Courses = (await _courseService.GetAllAsync()).Data;
            return View(result.Data);
        }

        // ✅ Güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Update(UpdateLessonDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = (await _courseService.GetAllAsync()).Data;
                return View(dto);
            }

            await _lessonService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ❌ Ders sil
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _lessonService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // 📄 Bir kursun derslerini getir (isteğe bağlı endpoint)
        [HttpGet]
        public async Task<IActionResult> ByCourse(int courseId)
        {
            var result = await _lessonService.GetLessonsByCourseIdAsync(courseId);
            return View("Index", result.Data);
        }
    }
}
