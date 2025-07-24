using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserManagement.Application.Dtos
{
    public class StudentDto
    {

        public int StudentID { get; set; }
        public string StudentName { get; set; } = string.Empty;

        public string IndexNumber { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string ContactNumber { get; set; } = string.Empty;

        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; } = string.Empty;
        public string? ParentContactNumber { get; set; }
        public string? ParentName { get; set; }
        public int Grade { get; set; }

    }

}
