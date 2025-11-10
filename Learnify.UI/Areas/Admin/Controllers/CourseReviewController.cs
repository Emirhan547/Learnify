using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.CourseReviewDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class CourseReviewController : Controller
    {
        private readonly ICourseReviewService _courseReviewService;
        private readonly ICourseService _courseService;

        public CourseReviewController(
            ICourseReviewService courseReviewService,
            ICourseService courseService)
        {
            _courseReviewService = courseReviewService;
            _courseService = courseService;
        }

        // 📋 Tüm kurs yorumlarını listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _courseReviewService.GetAllAsync();
            return View(result.Data);
        }

        // 🗂️ Belirli kursun yorumlarını listele
        [HttpGet]
        public async Task<IActionResult> ByCourse(int courseId)
        {
            var result = await _courseReviewService.GetByCourseIdAsync(courseId);
            return View("Index", result.Data);
        }

        // 💬 Yeni yorum oluşturma sayfası
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var courses = await _courseService.GetAllAsync();
            return View(courses.Data); // dropdown için ViewModel değil, doğrudan dto listesi
        }

        // 💾 Yeni yorum ekle
        [HttpPost]
        public async Task<IActionResult> Create(CreateCourseReviewDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _courseReviewService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ✏️ Yorum güncelleme sayfası
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _courseReviewService.GetForUpdateAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        // ✅ Yorum güncelle
        [HttpPost]
        public async Task<IActionResult> Update(UpdateCourseReviewDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _courseReviewService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ❌ Yorum sil
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _courseReviewService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
