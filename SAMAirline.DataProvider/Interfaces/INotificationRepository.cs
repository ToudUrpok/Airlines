using SAMAirline.DataProvider.Entities;
using System.Collections.Generic;

namespace SAMAirline.DataProvider.Interfaces
{
    public interface INotificationRepository : IRepository<Notification>
    {
        Notification GetNotification(int notificationId); 
        IEnumerable<Notification> GetUsersNotifications(int userId);
        void DeleteAllNotifications(int userId);
        void NewNotification(int userId, string message);
        void NewNotifications(IEnumerable<Booking> bookings);
        void DeleteNotification(int notificatioId);
    }
}
 