using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Repositories
{
    public interface IEmailService
    {

         public  Task SendEmailToUserAsync( String receptor, String subject, String body);
    }
}
