using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Dtos
{
    public class ResetPasswordRequestDto
    {
        public string Email { get; set; } = string.Empty;
    }
}
