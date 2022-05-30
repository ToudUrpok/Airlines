using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System.Collections.Generic;

namespace SAMAirline.Logic.Interfaces
{
    public interface INotificationService
    {
        NotificationWindowViewModel GetUsersNotifications(string email);
        void DeleteAllNotifications(int userId);
        void NewNotification(string email, int bookingId, int flightId, int delay);
        void DeleteNotification(int notificatioId);
        void NewNotifications(IEnumerable<BookingDto> bookings);
    }
}
