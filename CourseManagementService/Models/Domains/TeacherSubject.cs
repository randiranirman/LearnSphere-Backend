using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CourseManagementService.Models.Domains;

public partial class TeacherSubject
{
    [Key]
    [Column(Order = 1)]
    public Guid TeacherId { get; set; }

    [Key]
    [Column(Order = 2)]
    public Guid SubjectCode { get; set; }

    // navigation proparties
    public virtual Subject SubjectCodeNavigation { get; set; } = null!;

    public virtual Teacher Teacher { get; set; } = null!;
}
