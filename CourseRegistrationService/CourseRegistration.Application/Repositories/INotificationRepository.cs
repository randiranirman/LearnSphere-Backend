using CourseRegistration.Domain.Models;

namespace CourseRegistration.Application.Repositories
{
    public interface INotificationRepository
    {
        Task<List<Notification>> GetNotificationsByUserIdAsync(int userId, string? userRole = null, bool? isRead = null, int page = 1, int pageSize = 20);
        Task<List<Notification>> GetNotificationsByCategoryAsync(int userId, string category, bool? isRead = null);
        Task<List<Notification>> GetNotificationsByTypeAsync(int userId, string type, bool? isRead = null);
        Task<List<Notification>> GetNotificationsByDateRangeAsync(int userId, DateTime fromDate, DateTime toDate, bool? isRead = null);
        Task<Notification?> GetNotificationByIdAsync(int id);
        
        Task<int> CreateAsync(Notification notification);
        Task<int> CreateNotificationAsync(Notification notification);
        Task<bool> UpdateNotificationAsync(Notification notification);
        Task<bool> MarkAsReadAsync(int notificationId, int userId);
        Task<bool> MarkMultipleAsReadAsync(List<int> notificationIds, int userId);
        Task<bool> MarkAllAsReadAsync(int userId, string? userRole = null);
        Task<bool> DeleteNotificationAsync(int id);
        Task<bool> DeleteNotificationsByUserAsync(int userId);
        Task<int> GetUnreadCountAsync(int userId, string? userRole = null);
        Task<List<Notification>> GetRecentNotificationsAsync(int userId, int count = 5);
        Task<bool> CreateBulkNotificationsAsync(List<Notification> notifications);
        Task<bool> DeleteOldNotificationsAsync(DateTime cutoffDate);
    }
}
