using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SAMAirline.DataProvider.Repositories
{
    public class NotificationRepository : BaseRepository<Notification>, INotificationRepository
    {

        public NotificationRepository(AirlineDB context) : base(context)
        {
        }

        public void DeleteAllNotifications(int userId)
        {
            var list = DbSet
                .Where(b => b.UserId == userId)
                .ToList();
            foreach (var n in list)
                DbSet.Remove(n);
            Context.SaveChanges();
        }

        public void DeleteNotification(int notificatioId)
        {
            var notification = DbSet.
                Where(n => n.NotificationId == notificatioId).
                FirstOrDefault();
            if(notification != null)
                DbSet.Remove(notification);
            Context.SaveChanges();
        }

        public Notification GetNotification(int notificationId)
        {
            var notification = DbSet
                .Where(p => p.NotificationId == notificationId)
                .FirstOrDefault();

            if(notification == null)
                throw new ApplicationException($"Can't find notification with id = {notificationId}");

            return notification;
        }

        public IEnumerable<Notification> GetUsersNotifications(int userId)
        {
            return DbSet.Where(n => n.UserId == userId).ToList();
        }

        public void NewNotification(int userId, string message)
        {
            DbSet.Add(new Notification
            {
                 UserId = userId,
                 Message = message
            });
            Context.SaveChanges();
        }

        public void NewNotifications(IEnumerable<Booking> bookings)
        {
            var notifications = new List<Notification>();

            foreach(var b in bookings)
            {
                var user = Context.Users.FirstOrDefault(u => u.UserId == b.UserId);

                var flight = Context.Flights.Where(f => f.FlightId == b.FlightId).FirstOrDefault();
                var message = String.Format("Dear {0} {1}, you need to registrate tickets for flight from {2} to {3} departing {4} no later than 3 hours before departure",
                    user.Surname, user.Name, flight.DepartureAirportCode, flight.ArrivalAirportCode, flight.DepartureDateTime.ToString("d"));
                if (DbSet.Where(n => n.Message == message).FirstOrDefault() == null)
                {
                    notifications.Add(new Notification
                    {
                        UserId = user.UserId,
                        Message = message
                    });
                }
            }
            DbSet.AddRange(notifications);
            Context.SaveChanges();
        }
    }
}
