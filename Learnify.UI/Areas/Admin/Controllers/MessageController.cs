using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.MessageDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        // 📥 Gelen kutusu
        [HttpGet]
        public async Task<IActionResult> Inbox(int userId)
        {
            var result = await _messageService.GetInboxAsync(userId);
            return View(result.Data);
        }

        // 📤 Giden kutusu
        [HttpGet]
        public async Task<IActionResult> Sendbox(int userId)
        {
            var result = await _messageService.GetSentAsync(userId);
            return View(result.Data);
        }

        // 📝 Taslaklar
        [HttpGet]
        public async Task<IActionResult> Drafts(int userId)
        {
            var result = await _messageService.GetDraftsAsync(userId);
            return View(result.Data);
        }

        // 🚮 Çöp kutusu
        [HttpGet]
        public async Task<IActionResult> Trash(int userId)
        {
            var result = await _messageService.GetDeletedAsync(userId);
            return View(result.Data);
        }

        // 🚫 Spam kutusu
        [HttpGet]
        public async Task<IActionResult> Spam(int userId)
        {
            var result = await _messageService.GetSpamAsync(userId);
            return View(result.Data);
        }

        // ✉️ Yeni mesaj oluşturma sayfası
        [HttpGet]
        public IActionResult Create() => View();

        // ✅ Yeni mesaj gönder
        [HttpPost]
        public async Task<IActionResult> Create(CreateMessageDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _messageService.AddAsync(dto);
            return RedirectToAction(nameof(Sendbox), new { userId = dto.SenderId });
        }

        // 📄 Mesaj detay sayfası
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var result = await _messageService.GetByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        // 🗑️ Mesaj sil
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.DeleteAsync(id);
            return RedirectToAction(nameof(Trash));
        }
    }
}
