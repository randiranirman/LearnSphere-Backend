using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Dtos
{
    public  class RefreshTokenRequestDto
    {

        public int UserId { get; set; }
        public required string RefreshToken { get; set; }
    }
}
