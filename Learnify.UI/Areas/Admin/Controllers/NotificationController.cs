using Learnify.Business.Abstract;
using Learnify.DTO.DTOs.NotificationDto;
using Learnify.Entity.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Threading.Tasks;

namespace Learnify.UI.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class NotificationController : Controller
    {
        private readonly INotificationService _notificationService;
        private readonly UserManager<AppUser> _userManager;

        public NotificationController(INotificationService notificationService, UserManager<AppUser> userManager)
        {
            _notificationService = notificationService;
            _userManager = userManager;
        }

        private void LoadUsers()
        {
            var users = _userManager.Users
                .Where(u => u.IsActive)
                .Select(u => new { u.Id, u.FullName })
                .ToList();
            ViewBag.Users = new SelectList(users, "Id", "FullName");
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var notifications = await _notificationService.GetAllAsync();
            return View(notifications);
        }

        [HttpGet]
        public IActionResult Create()
        {
            LoadUsers();
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateNotificationDto dto)
        {
            if (!ModelState.IsValid)
            {
                LoadUsers();
                return View(dto);
            }

            await _notificationService.AddAsync(dto);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> GetUnreadPartial()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null) return PartialView("_NotificationDropdownPartial", new List<ResultNotificationDto>());

            var notifications = await _notificationService.GetUnreadAsync(user.Id);
            return PartialView("_NotificationDropdownPartial", notifications.Take(5).ToList());
        }
        [HttpPost]
        public async Task<IActionResult> MarkAsRead(int id)
        {
            await _notificationService.MarkAsReadAsync(id);
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var notif = await _notificationService.GetByIdAsync(id);
            if (notif == null) return NotFound();

            await _notificationService.MarkAsReadAsync(id);
            return View(notif);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _notificationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
