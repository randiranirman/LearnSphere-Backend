using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManagement.Application.Repositories;

namespace UserManagement.API.Controllers
{
    [Route("api/email")]
    [ApiController]
    public class EmailController(IEmailService emailService, IConfiguration configuration) : ControllerBase

    {

        [HttpPost("send")]

        public async Task<IActionResult> SendEmail(String receptor, String subject, String body)
        {
            await emailService.SendEmailToUserAsync(receptor, subject, body);


            return Ok(new { message = "Email sent successfully" });



        }

    }
}
