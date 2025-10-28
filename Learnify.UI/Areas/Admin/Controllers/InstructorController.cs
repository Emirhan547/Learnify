using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.InstructorDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InstructorController : Controller
    {
        private readonly IInstructorService _instructorService;

        public InstructorController(IInstructorService instructorService)
        {
            _instructorService = instructorService;
        }

        // 📋 Listeleme
        public async Task<IActionResult> Index()
        {
            var instructors = await _instructorService.GetAllAsync();
            return View(instructors);
        }

        // ➕ Yeni Eğitmen Formu
        [HttpGet]
        public IActionResult CreateInstructor() => View();

        // ✅ Yeni Eğitmen Kaydı
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateInstructor(CreateInstructorDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen tüm alanları doğru doldurun.";
                return View(dto);
            }

            try
            {
                await _instructorService.AddAsync(dto);
                TempData["Success"] = "Eğitmen başarıyla eklendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(dto);
            }
        }

        // ✏️ Güncelleme Formu
        [HttpGet]
        public async Task<IActionResult> UpdateInstructor(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
                return NotFound();

            var dto = new UpdateInstructorDto
            {
                Id = instructor.Id,
                UserName = instructor.UserName,
                FullName = instructor.FullName,
                Email = instructor.Email,
                Profession = instructor.Profession
            };

            return View(dto);
        }

        // ✅ Güncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInstructor(UpdateInstructorDto dto)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Lütfen geçerli bilgileri giriniz.";
                return View(dto);
            }

            try
            {
                await _instructorService.UpdateAsync(dto);
                TempData["Success"] = "Eğitmen bilgileri güncellendi.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return View(dto);
            }
        }

        // ❌ Sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            try
            {
                await _instructorService.DeleteAsync(id);
                TempData["Success"] = "Eğitmen başarıyla silindi.";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
