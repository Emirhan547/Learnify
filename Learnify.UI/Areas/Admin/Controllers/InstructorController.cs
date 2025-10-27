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
                return View(dto);

            await _instructorService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ✏️ Güncelleme Formu
        [HttpGet]
        public async Task<IActionResult> UpdateInstructor(int id)
        {
            var instructor = await _instructorService.GetByIdAsync(id);
            if (instructor == null)
                return NotFound();

            // 🔹 ResultInstructorDto -> UpdateInstructorDto manuel map
            var dto = new UpdateInstructorDto
            {
                Id = instructor.Id,
                UserName = instructor.UserName,
                FullName = instructor.FullName,
                Email = instructor.Email,
                Profession = instructor.Profession
            };

            return View(dto); // ✅ Artık View doğru modeli alıyor
        }


        // ✅ Güncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateInstructor(UpdateInstructorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _instructorService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ❌ Sil
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteInstructor(int id)
        {
            await _instructorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
