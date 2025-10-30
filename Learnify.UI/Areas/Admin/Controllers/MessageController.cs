using AutoMapper;
using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.MessageDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class MessageController : Controller
    {
        private readonly IMessageService _messageService;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public MessageController(IMessageService messageService, UserManager<AppUser> userManager, IMapper mapper)
        {
            _messageService = messageService;
            _userManager = userManager;
            _mapper = mapper;
        }

        private async Task<int> GetCurrentUserIdAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            return user?.Id ?? 0;
        }

        private void LoadReceiversDropdown()
        {
            ViewBag.Users = new SelectList(_userManager.Users.Where(u => u.IsActive).ToList(), "Id", "FullName");
        }

        // 📥 Gelen Kutusu
        [HttpGet]
        public async Task<IActionResult> Inbox()
        {
            var userId = await GetCurrentUserIdAsync();
            var messages = await _messageService.GetInboxAsync(userId);
            return View(messages);
        }

        // 📤 Gönderilenler
        [HttpGet]
        public async Task<IActionResult> Sent()
        {
            var userId = await GetCurrentUserIdAsync();
            var messages = await _messageService.GetSentAsync(userId);
            return View(messages);
        }

        // 📝 Taslaklar
        [HttpGet]
        public async Task<IActionResult> Draft()
        {
            var userId = await GetCurrentUserIdAsync();
            var messages = await _messageService.GetDraftsAsync(userId);
            return View(messages);
        }

        // 🗑️ Silinenler
        [HttpGet]
        public async Task<IActionResult> Trash()
        {
            var userId = await GetCurrentUserIdAsync();
            var messages = await _messageService.GetDeletedAsync(userId);
            return View(messages);
        }

        // 🚫 Spam
        [HttpGet]
        public async Task<IActionResult> Spam()
        {
            var userId = await GetCurrentUserIdAsync();
            var messages = await _messageService.GetSpamAsync(userId);
            return View(messages);
        }

        // 💌 Yeni Mesaj
        [HttpGet]
        public IActionResult NewMessage()
        {
            LoadReceiversDropdown();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> NewMessage(CreateMessageDto dto, string actionType)
        {
            var userId = await GetCurrentUserIdAsync();
            dto.SenderId = userId;

            if (actionType == "draft")
                dto.IsDraft = true;

            if (!ModelState.IsValid)
            {
                LoadReceiversDropdown();
                return View(dto);
            }

            await _messageService.AddAsync(dto);

            if (dto.IsDraft)
                return RedirectToAction(nameof(Draft));

            return RedirectToAction(nameof(Sent));
        }

        // 📄 Detay Görüntüleme
        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var message = await _messageService.GetByIdAsync(id);
            if (message == null)
                return NotFound();

            return View(message);
        }

        // ❌ Silme (soft delete)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _messageService.DeleteAsync(id);
            return RedirectToAction(nameof(Inbox));
        }
    }
}
