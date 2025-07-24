namespace UserManagement.Domain.DTOs;

public class GetAllStudentsRegisteredResponseDTO
{
    public string? IndexNumber { get; set; }
    public string? StudentFullName { get; set; }
    public string? StudentEmail { get; set; }
    public string? StudentMobileNumber { get; set; }
}
