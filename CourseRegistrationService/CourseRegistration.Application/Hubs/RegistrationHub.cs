using CourseRegistration.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CourseRegistration.Application.Services
{

    
    public class RegistrationHub : Hub
    {
        public async Task JoinStudentGroup(int studentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Student_{studentId}");
        }

        public async Task JoinTeacherGroup(int teacherId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Teacher_{teacherId}");
        }

        public async Task JoinAdminGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }

        public async Task LeaveStudentGroup(int studentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Student_{studentId}");
        }

        public async Task LeaveTeacherGroup(int teacherId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Teacher_{teacherId}");
        }

        public async Task LeaveAdminGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
         
        
    }
}
