using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Application.Dtos
{
    public class StudentListRequsetDto
    {

        public List<int> StudentDetailsRequestIds { get; set; } = new();
    }
}
