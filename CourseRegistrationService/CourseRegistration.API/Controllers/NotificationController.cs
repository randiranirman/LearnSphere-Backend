using CourseRegistration.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CourseRegistration.API.Controllers
{
    [Route("notification/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {


        [HttpPost("test-notification")]
        public async Task<IActionResult> TestNotification([FromServices] INotificationService notificationService)
        {
            await notificationService.NotifyNewRegistrationAsync(1, 1, "Test Class", new List<int> { 1, 2 }, new List<string> { "Math", "Science" }, "12345");
            return Ok("Notification sent");
        }

    }
}
