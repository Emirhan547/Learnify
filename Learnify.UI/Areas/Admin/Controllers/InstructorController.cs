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

        // 📋 Tüm eğitmenleri listele
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var result = await _instructorService.GetAllAsync();
            return View(result.Data);
        }

        // 🟢 Aktif eğitmenleri listele
        [HttpGet]
        public async Task<IActionResult> Active()
        {
            var result = await _instructorService.GetActiveInstructorsAsync();
            return View("Index", result.Data);
        }

        // ➕ Yeni eğitmen formu
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // 💾 Eğitmen ekle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateInstructorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _instructorService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ✏️ Eğitmen düzenleme sayfası
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _instructorService.GetByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            var updateDto = new UpdateInstructorDto
            {
                Id = result.Data.Id,
                FullName = result.Data.FullName,
                Email = result.Data.Email,
                Profession = result.Data.Profession,
                IsActive = result.Data.IsActive
            };

            return View(updateDto);
        }

        // ✅ Eğitmen güncelle
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(UpdateInstructorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _instructorService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index));
        }

        // ❌ Eğitmeni pasife al
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _instructorService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
