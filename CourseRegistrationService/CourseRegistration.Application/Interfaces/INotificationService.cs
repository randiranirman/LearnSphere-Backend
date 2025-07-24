using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CourseRegistration.Application.Interfaces
{
    public interface INotificationService
    {
        // Registration notifications
        Task NotifyNewRegistrationAsync(int studentId, int classId, string className, List<int> subjectIds, List<string> subjectNames, string indexNumber);
        Task NotifyNewRegistrationAsyncByTeacher(int teacherId, List<int> classIds , List<int> subjectIds);
        Task NotifyRegistrationApprovedAsync(int studentId, int registrationId, string className, List<string> subjectNames);

        Task NotifyRegistrationApprovedAsyncTeacher(int teacherId, List<int> subjectIds, string  employeeID, List<int> classIds);
        Task NotifyRegistrationRejectedAsync(int studentId, int registrationId, string className, List<string> subjectNames, string reason);

        Task NotifyNewRegistrationApprovedAsyncTeacher(int teacherId, List<int> subjectIds, string employeeID, List<int> classIds);
        
        // General notifications
        Task NotifyStudentAsync(int studentId, string title, string message, string type = "info");
        Task NotifyAdminsAsync(string title, string message, string type = "info");
        Task NotifyTeacherAsync(int teacherId, string title, string message, string type = "info");
        
        // Assignment notifications
        Task NotifyNewAssignmentAsync(int subjectId, string subjectName, string assignmentTitle, string assignmentDescription);
        
        // System notifications
        Task NotifySystemMessageAsync(string message, string type = "info");
        Task NotifyAllUsersAsync(string title, string message, string type = "info");
        
        // Additional registration notifications
        Task NotifyRegistrationCompletedAsync(int studentId, string className, string message);
        Task NotifyAdminsOnRegistrationAsync(int studentId, string className, List<string> subjectNames);
    }
}
