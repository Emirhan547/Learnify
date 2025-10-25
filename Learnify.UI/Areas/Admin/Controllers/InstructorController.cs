using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.InstructorDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        public async Task<IActionResult> Index()
        {
            var values = await _instructorService.GetAllAsync();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateInstructor()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateInstructor(CreateInstructorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _instructorService.AddAsync(dto);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateInstructor(int id)
        {
            var value = await _instructorService.GetByIdAsync(id);
            return View(value);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateInstructor(UpdateInstructorDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _instructorService.UpdateAsync(dto);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> DeleteInstructor(int id)
        {
            await _instructorService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
