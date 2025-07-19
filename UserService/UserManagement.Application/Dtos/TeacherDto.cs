using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Dtos
{
    public  class TeacherDto
    {

         public int TeacherID { get; set; }
        public string TeacherName { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;


        public string Fullname { get; set; } = string.Empty;

        public string Email { get; set; } = string.Empty;
            
    }
}
