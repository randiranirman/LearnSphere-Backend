using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Domain.Models
{
    public enum RegistrationStatus
    {

        Pending = 0,
        Approved = 1,
        Rejected = 2,
        Cancelled = 3
    }
}
