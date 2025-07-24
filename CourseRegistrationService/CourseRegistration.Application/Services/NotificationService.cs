using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CourseRegistration.Application.Interfaces;
using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;
using Microsoft.AspNetCore.SignalR;

namespace CourseRegistration.Application.Services
{
    public class NotificationService : INotificationService
    {
        private readonly IHubContext<RegistrationHub> _hubContext;
        private readonly INotificationRepository _notificationRepository;

        public NotificationService(IHubContext<RegistrationHub> hubContext, INotificationRepository notificationRepository)
        {
            _hubContext = hubContext;
            _notificationRepository = notificationRepository;
        }

        public async Task NotifyNewRegistrationAsync(int studentId, int classId, string className, List<int> subjectIds, List<string> subjectNames, string indexNumber)
        {
            // Create persistent notification for admins
            var notification = new Notification
            {
                Title = "New Registration Request",
                Message = $"Student {indexNumber} requested registration for class {className}",
                Type = "registration",
                TargetRole = "Admin",
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.CreateAsync(notification);

            // Send real-time notification with individual parameters (matches frontend)
            Console.WriteLine("notification send to admins ");
            await _hubContext.Clients.Group("Admins").SendAsync("NotifyNewRegistrationAsync",
                studentId, classId, className, subjectIds, subjectNames, indexNumber);
        }
        public async Task NotifyNewRegistrationAsyncByTeacher(int teacherId, List<int> classIds, List<int> subjectIds)
        {
            var notification = new Notification
            {
                Title = "New Registration Request by Teacher",
                Message = $"Teacher with ID {teacherId} has requested registration for classes {string.Join(", ", classIds)} and subjects {string.Join(", ", subjectIds)}.",
                Type = "registration",
                TargetRole = "Admin",
                CreatedAt = DateTime.UtcNow,
                IsRead = false

            };
            Console.WriteLine("notification send to admins by teacher ");
            await _hubContext.Clients.Group("Admins").SendAsync("NotifyNewRegistrationAsyncByTeacher",
                teacherId, classIds, subjectIds);



        }



        public async Task NotifyNewRegistrationApprovedAsyncTeacher(int teacherId, List<int> subjectIds, string employeeID, List<int> classIds)
        {
            var notification= new Notification
            {
                Title = "New Registration Approved",
                Message = $"Your registration for subjects {string.Join(", ", subjectIds)} has been approved.",
                Type = "approval",
                TargetRole = "Teacher",
                TargetUserId = teacherId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.CreateAsync(notification);
            // send real-time notification to teacher
            await _hubContext.Clients.Group($"Teacher_{teacherId}").SendAsync("NotifyNewRegistrationApprovedAsyncTeacher",
                teacherId, subjectIds, employeeID, classIds);

        }
        public Task NotifyRegistrationApprovedAsyncTeacher(int teacherId, List<int> subjectIds, int employeeID, List<int> classIds)
        {
            throw new NotImplementedException();
        }

        public async Task NotifyRegistrationApprovedAsync(int studentId, int registrationId, string className, List<string> subjectNames)
        {
            // Create persistent notification for student
            var notification = new Notification
            {
                Title = "Registration Approved",
                Message = $"Your registration for class {className} has been approved",
                Type = "approval",
                TargetRole = "Student",
                TargetUserId = studentId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.CreateAsync(notification);

            // Send real-time notification with individual parameters (matches frontend)
            Console.WriteLine("notification send to student ");
            await _hubContext.Clients.Group($"Student_{studentId}").SendAsync("NotifyRegistrationApproved",
                studentId, registrationId, className, subjectNames);
        }

        public async Task NotifyRegistrationRejectedAsync(int studentId, int registrationId, string className, List<string> subjectNames, string reason)
        {
            // Create persistent notification for student
            var notification = new Notification
            {
                Title = "Registration Rejected",
                Message = $"Your registration for class {className} has been rejected. Reason: {reason}",
                Type = "rejection",
                TargetRole = "Student",
                TargetUserId = studentId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.CreateAsync(notification);

            // Fixed method name and parameters to match frontend
            await _hubContext.Clients.Group($"Student_{studentId}").SendAsync("NotifyRegistrationRejected",
                studentId, registrationId, className, subjectNames, reason);
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

        public async Task NotifyRegistrationCompletedAsync(int studentId, string className, string message)
        {
            // Create persistent notification for students
            var notification = new Notification
            {
                Title = "Registration Completed",
                Message = message,
                Type = "completion",
                TargetRole = "Student",
                TargetUserId = studentId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.CreateAsync(notification);

            // Send real-time notification
            await _hubContext.Clients.Group($"Student_{studentId}").SendAsync("NotifyRegistrationCompleted",
                studentId, className, message);
        }

        public async Task NotifyAdminsOnRegistrationAsync(int studentId, string className, List<string> subjectNames)
        {
            var message = $"Student {studentId} registered for class {className} with subjects {string.Join(", ", subjectNames)}";

            var notification = new Notification
            {
                Title = "Student Registered",
                Message = message,
                Type = "info",
                TargetRole = "Admin",
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };

            await _notificationRepository.CreateAsync(notification);

            // Send real-time notification
            await _hubContext.Clients.Group("Admins").SendAsync("NotifyAdminsOnRegistration",
                studentId, className, subjectNames);
        }

        public async Task NotifyRegistrationApprovedAsyncTeacher(int teacherId, List<int> subjectIds, string employeeID, List<int> classIds)
        {
            var message = $"Your registration for subjects {string.Join(", ", subjectIds)} has been approved.";
            var notification = new Notification
            {
                Title = "Registration Approved",
                Message = message,
                Type = "approval",
                TargetRole = "Teacher",
                TargetUserId = teacherId,
                CreatedAt = DateTime.UtcNow,
                IsRead = false
            };
            await _hubContext.Clients.Group($"Teacher_{teacherId}").SendAsync("NotifyRegistrationApprovedAsyncTeacher",
                teacherId, subjectIds, employeeID, classIds);
        }

        public Task NotifyNewRegistrationApprovedAsyncTeacher(int teacherId, List<int> subjectIds, int employeeID, List<int> classIds)
        {
            throw new NotImplementedException();
        }
    }
}