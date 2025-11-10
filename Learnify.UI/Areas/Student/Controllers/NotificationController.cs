using Learnify.Business.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Learnify.UI.Areas.Student.Controllers
{
    [Area("Student")]
    [Authorize(Roles = "Student")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var result = await _notificationService.GetAllByUserIdAsync(userId);

            if (!result.Success || result.Data == null)
                return View(new List<DTO.DTOs.NotificationDto.ResultNotificationDto>());

            var notifications = result.Data
                .OrderByDescending(x => x.CreatedDate)
                .ToList();

            return View(notifications);
        }

        [HttpGet]
        public async Task<IActionResult> Detail(int id)
        {
            var result = await _notificationService.GetByIdAsync(id);
            if (!result.Success || result.Data == null)
                return RedirectToAction(nameof(Index));

            return View(result.Data);
        }
    }
}
