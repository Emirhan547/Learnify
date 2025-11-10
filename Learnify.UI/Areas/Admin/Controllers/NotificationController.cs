using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.NotificationDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        // 🔔 Tüm bildirimleri listele
        [HttpGet]
        public async Task<IActionResult> Index(int userId)
        {
            var result = await _notificationService.GetAllByUserIdAsync(userId);
            return View(result.Data);
        }

        // 📬 Okunmamış bildirimleri listele
        [HttpGet]
        public async Task<IActionResult> Unread(int userId)
        {
            var result = await _notificationService.GetUnreadAsync(userId);
            return View(result.Data);
        }

        // ✅ Bildirimi okundu olarak işaretle
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return RedirectToAction(nameof(Index));
        }

        // ➕ Yeni bildirim oluşturma sayfası
        [HttpGet]
        public IActionResult Create() => View();

        // ✅ Yeni bildirim oluştur
        [HttpPost]
        public async Task<IActionResult> Create(CreateNotificationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _notificationService.AddAsync(dto);
            return RedirectToAction(nameof(Index), new { userId = dto.UserId });
        }

        // ✏️ Bildirim düzenleme formu
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var result = await _notificationService.GetByIdAsync(id);
            if (!result.Success || result.Data == null)
                return NotFound();

            return View(result.Data);
        }

        // ✅ Bildirim güncelle
        [HttpPost]
        public async Task<IActionResult> Update(UpdateNotificationDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            await _notificationService.UpdateAsync(dto);
            return RedirectToAction(nameof(Index), new { userId = dto.UserId });
        }

        // ❌ Bildirimi sil
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _notificationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
