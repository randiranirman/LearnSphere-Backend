﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CourseRegistration.Application.Dtos
{
    public  class CreateClassRequset
    {
        public string Name { get; set; } = string.Empty;    
        public string Code { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public int Credit { get; set; }

    }
}
