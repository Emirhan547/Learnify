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

        public async Task<IActionResult> Index()
        {
            var studentId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var notifications = await _notificationService.GetAllByUserIdAsync(studentId);

            return View(notifications.OrderByDescending(x => x.CreatedDate).ToList());
        }

        public async Task<IActionResult> Detail(int id)
        {
            var notification = await _notificationService.GetByIdAsync(id);
            if (notification == null) return RedirectToAction("Index");

            return View(notification);
        }
    }
}
