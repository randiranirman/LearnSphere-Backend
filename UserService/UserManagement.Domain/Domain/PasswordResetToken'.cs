using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Domain.Domain
{
    public  class PasswordResetToken
    {

        public int Id;
        public string? UserEmail;
        public string? Token;
        public DateTime ExpiryDate;



    }
}
