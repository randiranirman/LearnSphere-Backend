using Microsoft.AspNetCore.SignalR;

namespace CourseRegistration.Application.Services
{
    public class RegistrationHub : Hub
    {
        public async Task JoinStudentGroup(int studentId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, $"Student_{studentId}");
        }

        public async Task JoinAdminGroup()
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, "Admins");
        }

        public async Task LeaveStudentGroup(int studentId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, $"Student_{studentId}");
        }

        public async Task LeaveAdminGroup()
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "Admins");
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await base.OnDisconnectedAsync(exception);
        }
    }
}
