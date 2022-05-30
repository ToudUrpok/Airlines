using SAMAirline.DataProvider.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SAMAirline.DataProvider.Interfaces
{
    public interface IBookingRepository : IRepository<Booking>
    {
        IEnumerable<Booking> GetDefaultUsersBookings();
        IEnumerable<Booking> GetAllByFlight(int flightId);
        Booking NewBooking(Booking booking);
        Booking GetBooking(int bookingId);
        Booking CancelBooking(int bookingId);
        IEnumerable<Booking> GetUserBookings(int? userId);
        bool IsAlreadyBooked(int userId, int flightId, int amount);
        void DeleteAllBookings(int userId);
        Booking GetBooking(int userId, int flightId);
        void IsThereAccess(int userId, int? bookingId);
        void ChangeStatus(int bookingId, bool status);
        string GetOwner(int bookingId);
    }
}
