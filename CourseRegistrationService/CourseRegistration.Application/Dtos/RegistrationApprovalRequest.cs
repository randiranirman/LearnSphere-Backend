using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Application.Dtos
{
    public   class RegistrationApprovalRequest
    {
        public int  RegistrationId { get; set; }
        public int AdminId { get; set; }
        public bool IsApproved { get; set; }
        public string? Remark { get; set; } = string.Empty;

    }
}
