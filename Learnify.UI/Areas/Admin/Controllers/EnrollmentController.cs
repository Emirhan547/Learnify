using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.EnrollmentDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class EnrollmentController : Controller
    {
        private readonly IEnrollmentService _enrollmentService;
        private readonly ICourseService _courseService;
        private readonly IInstructorService _instructorService;

        public EnrollmentController(
            IEnrollmentService enrollmentService,
            ICourseService courseService,
            IInstructorService instructorService)
        {
            _enrollmentService = enrollmentService;
            _courseService = courseService;
            _instructorService = instructorService;
        }

        // 📄 Tüm kayıtları listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _enrollmentService.GetAllWithCourseAndStudentAsync();
            return View(result.Data);
        }

        // ➕ Yeni kayıt formu
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.Courses = (await _courseService.GetAllAsync()).Data;
            ViewBag.Students = (await _instructorService.GetAllAsync()).Data; // AppUser tabanlı öğrenci listesi ileride değişebilir
            return View();
        }

        // ✅ Yeni kayıt oluştur
        [HttpPost]
        public async Task<IActionResult> Create(CreateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = (await _courseService.GetAllAsync()).Data;
                ViewBag.Students = (await _instructorService.GetAllAsync()).Data;
                return View(dto);
            }

            await _enrollmentService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ✏️ Güncelleme sayfası
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _enrollmentService.GetByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            ViewBag.Courses = (await _courseService.GetAllAsync()).Data;
            ViewBag.Students = (await _instructorService.GetAllAsync()).Data;
            return View(result.Data);
        }

        // ✅ Güncelleme işlemi
        [HttpPost]
        public async Task<IActionResult> Update(UpdateEnrollmentDto dto)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.Courses = (await _courseService.GetAllAsync()).Data;
                ViewBag.Students = (await _instructorService.GetAllAsync()).Data;
                return View(dto);
            }

            await _enrollmentService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ❌ Kayıt sil
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _enrollmentService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // 👨‍🎓 Öğrenciyi kursa kaydet (opsiyonel)
        [HttpPost]
        public async Task<IActionResult> Enroll(int courseId, int studentId)
        {
            await _enrollmentService.EnrollStudentAsync(courseId, studentId);
            return RedirectToAction(nameof(Index));
        }

        // 🔎 Kursa göre filtreleme
        [HttpGet]
        public async Task<IActionResult> ByCourse(int courseId)
        {
            var result = await _enrollmentService.GetAllAsync();
            var filtered = result.Data?.FindAll(x => x.CourseId == courseId);
            return View("Index", filtered);
        }
    }
}
