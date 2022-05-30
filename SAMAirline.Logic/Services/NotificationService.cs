using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IPassengerRepository _passengerRepository;
        private readonly IFlightRepository _flightRepository;
        private readonly IMapper<BookingDto, Booking> _mapperBooking;

        public NotificationService(
            INotificationRepository notificationRepository,
            IBookingRepository bookingRepository,
            IPassengerRepository passengerRepository,
            IFlightRepository flightRepository,
            IMapper<BookingDto, Booking> mapperBooking)
        {
            _notificationRepository = notificationRepository;
            _bookingRepository = bookingRepository;
            _passengerRepository = passengerRepository;
            _flightRepository = flightRepository;
            _mapperBooking = mapperBooking;
        }

        public void DeleteAllNotifications(int userId)
        {
            _notificationRepository.DeleteAllNotifications(userId);
        }

        public void DeleteNotification(int notificatioId)
        {
            _notificationRepository.DeleteNotification(notificatioId);
        }

        public NotificationWindowViewModel GetUsersNotifications(string email)
        {
            var user = _passengerRepository.GetByEmail(email);
            var userId = user.UserId;

            var list = _notificationRepository.GetUsersNotifications(userId).Distinct();
            var notifications = list.Select(n => new NotificationDto
            {
                NotificationId = n.NotificationId,
                UserId = n.UserId,
                Message = n.Message
            }).ToList();
            var notView = new NotificationWindowViewModel
            {
                List = notifications,
                Count = notifications.Count()
            };
            return notView;
        }

        public void NewNotifications(IEnumerable<BookingDto> bookings)
        {
            _notificationRepository.NewNotifications(bookings.Select(b => _mapperBooking.MapToDalEntity(b)));
        }

        public void NewNotification(string email, int bookingId = 0, int flightId = 0, int delay = 0)
        {
            string message = "";
            var user = _passengerRepository.GetByEmail(email);
            if (bookingId != 0 && flightId == 0)
            {
                var booking = _bookingRepository.GetBooking(bookingId);
                var flight = _flightRepository.GetFlight(booking.FlightId);
                message += String.Format("Dear {0} {1}, we apologize but your booking on flight from {2} to {3} at {4} has been deleted by the moderator.",
                    user.Surname, user.Name, flight.DepartureAirportCode, flight.ArrivalAirportCode, flight.DepartureDateTime);
            }
            else if (flightId != 0 && delay != 0)
            {
                var flight = _flightRepository.GetFlight(flightId);
                message += String.Format("Dear {0} {1}, we apologize but flight from {2} to {3} departing {4} has been delayed by {5} hours and now departing at {6}",
                    user.Surname, user.Name, flight.DepartureAirportCode, flight.ArrivalAirportCode, flight.DepartureDateTime.ToString("d"), delay, flight.DepartureDateTime.ToString("t"));
            }
            _notificationRepository.NewNotification(user.UserId, message);
        }
    }
}
