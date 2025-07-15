using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using UserManagement.Application.Repositories;

namespace UserManagement.Infrastructure.Services
{
    public class EmailService( IConfiguration configuration) : IEmailService
    {
        public async Task SendEmailToUserAsync(string receptor, string subject, string body)
        {
            var email = configuration.GetValue<string>("EMAIL_CONFIGURATION:EMAIL");
            var password = configuration.GetValue<string>("EMAIL_CONFIGURATION:PASSWORD");
            var host = configuration.GetValue<string>("EMAIL_CONFIGURATION:HOST");
            var port = configuration.GetValue<int>("EMAIL_CONFIGURATION:PORT");

            using var smtpClient = new SmtpClient(host, port)
            {
                EnableSsl = true,
                Credentials = new NetworkCredential(email, password),
                Timeout = 30000 // 30 seconds timeout
            };

            using var message = new MailMessage(email!, receptor, subject, body);
            
            await smtpClient.SendMailAsync(message);
        }
    }
}
