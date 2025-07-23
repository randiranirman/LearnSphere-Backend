using CourseRegistration.Application.Repositories;
using CourseRegistration.Domain.Models;

namespace CourseRegistration.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private static readonly List<Notification> _notifications = new List<Notification>();
        private static int _nextId = 1;
        private static readonly object _lock = new object();

        public Task<List<Notification>> GetNotificationsByUserIdAsync(int userId, string? userRole = null, bool? isRead = null, int page = 1, int pageSize = 20)
        {
            lock (_lock)
            {
                var query = _notifications.Where(n => n.UserId == userId && !n.IsDeleted);

                if (!string.IsNullOrEmpty(userRole))
                    query = query.Where(n => n.UserRole == userRole);

                if (isRead.HasValue)
                    query = query.Where(n => n.IsRead == isRead.Value);

                var result = query
                    .OrderByDescending(n => n.CreatedAt)
                    .Skip((page - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return Task.FromResult(result);
            }
        }

        public Task<int> CreateAsync(Notification notification)
        {
            return CreateNotificationAsync(notification);
        }

        public Task<List<Notification>> GetNotificationsByCategoryAsync(int userId, string category, bool? isRead = null)
        {
            lock (_lock)
            {
                var query = _notifications.Where(n => n.UserId == userId && n.Category == category && !n.IsDeleted);

                if (isRead.HasValue)
                    query = query.Where(n => n.IsRead == isRead.Value);

                var result = query.OrderByDescending(n => n.CreatedAt).ToList();
                return Task.FromResult(result);
            }
        }

        public Task<List<Notification>> GetNotificationsByTypeAsync(int userId, string type, bool? isRead = null)
        {
            lock (_lock)
            {
                var query = _notifications.Where(n => n.UserId == userId && n.Type == type && !n.IsDeleted);

                if (isRead.HasValue)
                    query = query.Where(n => n.IsRead == isRead.Value);

                var result = query.OrderByDescending(n => n.CreatedAt).ToList();
                return Task.FromResult(result);
            }
        }

        public Task<List<Notification>> GetNotificationsByDateRangeAsync(int userId, DateTime fromDate, DateTime toDate, bool? isRead = null)
        {
            lock (_lock)
            {
                var query = _notifications.Where(n => n.UserId == userId &&
                    n.CreatedAt >= fromDate && n.CreatedAt <= toDate && !n.IsDeleted);

                if (isRead.HasValue)
                    query = query.Where(n => n.IsRead == isRead.Value);

                var result = query.OrderByDescending(n => n.CreatedAt).ToList();
                return Task.FromResult(result);
            }
        }

        public Task<Notification?> GetNotificationByIdAsync(int id)
        {
            lock (_lock)
            {
                var notification = _notifications.FirstOrDefault(n => n.Id == id && !n.IsDeleted);
                return Task.FromResult(notification);
            }
        }

        public Task<int> CreateNotificationAsync(Notification notification)
        {
            lock (_lock)
            {
                notification.Id = _nextId++;
                notification.CreatedAt = DateTime.UtcNow;
                _notifications.Add(notification);
                return Task.FromResult(notification.Id);
            }
        }

        public Task<bool> UpdateNotificationAsync(Notification notification)
        {
            lock (_lock)
            {
                var existing = _notifications.FirstOrDefault(n => n.Id == notification.Id && !n.IsDeleted);
                if (existing == null) return Task.FromResult(false);

                existing.Title = notification.Title;
                existing.Message = notification.Message;
                existing.Type = notification.Type;
                existing.IsRead = notification.IsRead;
                existing.ReadAt = notification.ReadAt;
                existing.Category = notification.Category;
                existing.ReferenceId = notification.ReferenceId;
                existing.ReferenceType = notification.ReferenceType;
                existing.ActionUrl = notification.ActionUrl;
                existing.Data = notification.Data;

                return Task.FromResult(true);
            }
        }

        public Task<bool> MarkAsReadAsync(int notificationId, int userId)
        {
            lock (_lock)
            {
                var notification = _notifications.FirstOrDefault(n => n.Id == notificationId && n.UserId == userId && !n.IsDeleted);
                if (notification == null) return Task.FromResult(false);

                notification.IsRead = true;
                notification.ReadAt = DateTime.UtcNow;
                return Task.FromResult(true);
            }
        }

        public Task<bool> MarkMultipleAsReadAsync(List<int> notificationIds, int userId)
        {
            lock (_lock)
            {
                var notifications = _notifications.Where(n => notificationIds.Contains(n.Id) && n.UserId == userId && !n.IsDeleted).ToList();

                foreach (var notification in notifications)
                {
                    notification.IsRead = true;
                    notification.ReadAt = DateTime.UtcNow;
                }

                return Task.FromResult(notifications.Any());
            }
        }

        public Task<bool> MarkAllAsReadAsync(int userId, string? userRole = null)
        {
            lock (_lock)
            {
                var query = _notifications.Where(n => n.UserId == userId && !n.IsRead && !n.IsDeleted);

                if (!string.IsNullOrEmpty(userRole))
                    query = query.Where(n => n.UserRole == userRole);

                var notifications = query.ToList();

                foreach (var notification in notifications)
                {
                    notification.IsRead = true;
                    notification.ReadAt = DateTime.UtcNow;
                }

                return Task.FromResult(notifications.Any());
            }
        }

        public Task<bool> DeleteNotificationAsync(int id)
        {
            lock (_lock)
            {
                var notification = _notifications.FirstOrDefault(n => n.Id == id && !n.IsDeleted);
                if (notification == null) return Task.FromResult(false);

                notification.IsDeleted = true;
                return Task.FromResult(true);
            }
        }

        public Task<bool> DeleteNotificationsByUserAsync(int userId)
        {
            lock (_lock)
            {
                var notifications = _notifications.Where(n => n.UserId == userId && !n.IsDeleted).ToList();

                foreach (var notification in notifications)
                {
                    notification.IsDeleted = true;
                }

                return Task.FromResult(notifications.Any());
            }
        }

        public Task<int> GetUnreadCountAsync(int userId, string? userRole = null)
        {
            lock (_lock)
            {
                var query = _notifications.Where(n => n.UserId == userId && !n.IsRead && !n.IsDeleted);

                if (!string.IsNullOrEmpty(userRole))
                    query = query.Where(n => n.UserRole == userRole);

                var count = query.Count();
                return Task.FromResult(count);
            }
        }

        public Task<List<Notification>> GetRecentNotificationsAsync(int userId, int count = 5)
        {
            lock (_lock)
            {
                var notifications = _notifications
                    .Where(n => n.UserId == userId && !n.IsDeleted)
                    .OrderByDescending(n => n.CreatedAt)
                    .Take(count)
                    .ToList();

                return Task.FromResult(notifications);
            }
        }

        public Task<bool> CreateBulkNotificationsAsync(List<Notification> notifications)
        {
            lock (_lock)
            {
                var createdAt = DateTime.UtcNow;

                foreach (var notification in notifications)
                {
                    notification.Id = _nextId++;
                    notification.CreatedAt = createdAt;
                    _notifications.Add(notification);
                }

                return Task.FromResult(true);
            }
        }

        public Task<bool> DeleteOldNotificationsAsync(DateTime cutoffDate)
        {
            lock (_lock)
            {
                var oldNotifications = _notifications.Where(n => n.CreatedAt < cutoffDate && !n.IsDeleted).ToList();

                foreach (var notification in oldNotifications)
                {
                    notification.IsDeleted = true;
                }

                return Task.FromResult(oldNotifications.Any());
            }
        }
    }
}