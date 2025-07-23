using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("notification/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationRepository notificationRepository, INotificationService notificationService)
        {
            _notificationRepository = notificationRepository;
            _notificationService = notificationService;
        }

        [HttpGet("{userId}/notifications")]
        public async Task<IActionResult> GetNotificationsByUserId(int userId, [FromQuery] string? userRole = null, [FromQuery] bool? isRead = null, [FromQuery] int page = 1, [FromQuery] int pageSize = 20)
        {
            try
            {
                var notifications = await _notificationRepository.GetNotificationsByUserIdAsync(userId, userRole, isRead, page, pageSize);
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{userId}/unread-count")]
        public async Task<IActionResult> GetUnreadCount(int userId, [FromQuery] string? userRole = null)
        {
            try
            {
                var count = await _notificationRepository.GetUnreadCountAsync(userId, userRole);
                return Ok(new { unreadCount = count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{notificationId}/mark-read")]
        public async Task<IActionResult> MarkAsRead(int notificationId, [FromQuery] int userId)
        {
            try
            {
                var result = await _notificationRepository.MarkAsReadAsync(notificationId, userId);
                if (result)
                {
                    return Ok(new { message = "Notification marked as read" });
                }
                return BadRequest("Failed to mark notification as read");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{userId}/mark-all-read")]
        public async Task<IActionResult> MarkAllAsRead(int userId, [FromQuery] string? userRole = null)
        {
            try
            {
                var result = await _notificationRepository.MarkAllAsReadAsync(userId, userRole);
                if (result)
                {
                    return Ok(new { message = "All notifications marked as read" });
                }
                return BadRequest("Failed to mark all notifications as read");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPost("test-notification")]
        public async Task<IActionResult> TestNotification()
        {
            await _notificationService.NotifyNewRegistrationAsync(1, 1, "Test Class", new List<int> { 1, 2 }, new List<string> { "Math", "Science" }, "12345");
            return Ok("Notification sent");
        }

        [HttpPost("test-registration-approval")]
        public async Task<IActionResult> TestRegistrationApproval([FromQuery] int studentId = 1, [FromQuery] string className = "Test Class")
        {
            await _notificationService.NotifyRegistrationApprovedAsync(studentId, 1, className, new List<string> { "Math", "Science" });
            return Ok("Registration approval notification sent");
        }

        [HttpPost("test-admin-notification")]
        public async Task<IActionResult> TestAdminNotification([FromQuery] int studentId = 1, [FromQuery] string className = "Test Class")
        {
            await _notificationService.NotifyAdminsOnRegistrationAsync(studentId, className, new List<string> { "Math", "Science" });
            return Ok("Admin notification sent");
        }
    }
}
