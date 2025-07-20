using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseRegistration.Application.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CourseRegistration.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<RegistrationHub> _hubContext;

        public NotificationService(IHubContext<RegistrationHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyNewRegistrationAsync(int studentId, int classId, string className, List<int> subjectIds, List<string> subjectNames, string indexNumber)
        {
            Console.WriteLine("notification send to admins ");
            await _hubContext.Clients.Group("Admins").SendAsync("NotifyNewRegistrationAsync", new
            {
                StudentId = studentId,
                ClassId = classId,
                ClassName = className,
                SubjectIds = subjectIds,
                SubjectNames = subjectNames,
                IndexNumber = indexNumber
            });
        }

        public async Task NotifyRegistrationApprovedAsync(int studentId, int registrationId, string className, List<string> subjectNames)
        {
            Console.WriteLine("notification send to admins ");
            await _hubContext.Clients.Group($"Student_{studentId}").SendAsync("NotifyRegistrationApproved", new
            {
                RegistrationId = registrationId,
                ClassName = className,
                SubjectNames = subjectNames
            });
        }

        public async Task NotifyRegistrationRejectedAsync(int studentId, int registrationId, string className, List<string> subjectNames, string reason)
        {
            await _hubContext.Clients.Group($"Student_{studentId}").SendAsync("RegistrationRejected", new
            {
                RegistrationId = registrationId,
                ClassName = className,
                SubjectNames = subjectNames,
                Reason = reason
            });
        }

        public Task NotifyStudentAsync(int studentId, string title, string message, string type = "info")
        {
            // Implementation for notifying individual students
            return Task.CompletedTask;
        }

        public Task NotifyAdminsAsync(string title, string message, string type = "info")
        {
            // Implementation for notifying admins
            return Task.CompletedTask;
        }
        
        public Task NotifyTeacherAsync(int teacherId, string title, string message, string type = "info")
        {
            // Implementation for notifying teachers
            return Task.CompletedTask;
        }

        public Task NotifyNewAssignmentAsync(int subjectId, string subjectName, string assignmentTitle, string assignmentDescription)
        {
            // Implementation for notifying about new assignments
            return Task.CompletedTask;
        }

        public Task NotifySystemMessageAsync(string message, string type = "info")
        {
            // Implementation for system-wide notifications
            return Task.CompletedTask;
        }

        public Task NotifyAllUsersAsync(string title, string message, string type = "info")
        {
            // Implementation for notifying all users
            return Task.CompletedTask;
        }
    }
}
