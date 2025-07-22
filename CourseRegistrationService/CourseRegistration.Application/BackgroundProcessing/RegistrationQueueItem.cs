using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;

namespace CourseRegistration.Application.BackgroundProcessing
{
    public class RegistrationQueueItem
    {
        public StudentRegistrationRequestDto RequestDto { get; set; }
        public TaskCompletionSource<StudentRegistrationResponseDto> ResponseTcs { get; set; }
    }
}
