using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CourseRegistration.Application.Dtos;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Interfaces
{
    public interface IYearlyRegistrationService
    {
        // Student Registration
        Task<int> RegisterStudentAsync(StudentYearlyRegistrationRequest request);
        Task<IEnumerable<StudentClassRegistration>> GetStudentRegistrationsAsync(int studentId);
        Task<IEnumerable<StudentClassRegistration>> GetPendingStudentRegistrationsAsync();
        
        // Teacher Registration
        Task<int> RegisterTeacherAsync(TeacherYearlyRegistrationRequest request);
        Task<IEnumerable<TeacherClassRegistration>> GetTeacherRegistrationsAsync(int teacherId);
        Task<IEnumerable<TeacherClassRegistration>> GetPendingTeacherRegistrationsAsync();
        
        // Admin Approval
        Task<bool> ApproveStudentRegistrationAsync(RegistrationApprovalRequest request);
        Task<bool> ApproveTeacherRegistrationAsync(RegistrationApprovalRequest request);
        Task<bool> RejectStudentRegistrationAsync(RegistrationApprovalRequest request);
        Task<bool> RejectTeacherRegistrationAsync(RegistrationApprovalRequest request);
        
        // Validation
        Task<bool> ValidateStudentRegistrationAsync(StudentYearlyRegistrationRequest request);
        Task<bool> ValidateTeacherRegistrationAsync(TeacherYearlyRegistrationRequest request);
        Task<bool> CanStudentRegisterForClassAsync(int studentId, int classId);
        Task<bool> CanTeacherRegisterForClassAsync(int teacherId, int classId);
        
        // Reports
        Task<IEnumerable<StudentClassRegistration>> GetAllStudentRegistrationsAsync();
        Task<IEnumerable<TeacherClassRegistration>> GetAllTeacherRegistrationsAsync();
        Task<IEnumerable<StudentClassRegistration>> GetStudentRegistrationsByStatusAsync(RegistrationStatus status);
        Task<IEnumerable<TeacherClassRegistration>> GetTeacherRegistrationsByStatusAsync(RegistrationStatus status);
    }
}

