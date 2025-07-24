namespace UserManagement.Domain.DTOs;

public class GetAllTeachersResponseDTO
{
    public string? EmployeeId { get; set; }
    public string? TeacherFullName { get; set; }
    public string? TeacherEmail { get; set; }
    public string? TeacherMobileNumber { get; set; }
}
