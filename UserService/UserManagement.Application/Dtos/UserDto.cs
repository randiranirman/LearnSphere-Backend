using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Dtos
{
    public class UserDto
    {

        public string Name { get; set; } = string.Empty;
        public string Username { get; set; }
        = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
