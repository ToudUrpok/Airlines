using SAMAirline.Common;
using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace SAMAirline.DataProvider.Repositories
{
    public class BookingRepository : BaseRepository<Booking>, IBookingRepository
    {
        public BookingRepository(AirlineDB context) : base(context)
        {
        }

        public IEnumerable<Booking> GetDefaultUsersBookings()
        {
            return DbSet
                .Include(b => b.User)
                .Include(b => b.Flight)
                .Where(b => b.User.Role != Constants.AdminRole);
        }

        public Booking GetBooking(int Id)
        {
            var booking = DbSet
                .Include(b => b.Flight)
                .Include(b => b.User)
                .Where(b => b.BookingId == Id)
                .FirstOrDefault();

            if (booking == null)
                throw new ApplicationException($"Can't find booking with id = {Id}");

            return booking;
        }

        public Booking CancelBooking(int bookingId)
        {
            var booking = DbSet
                .Where(b => b.BookingId == bookingId)
                .FirstOrDefault();

            if (booking == null)
                throw new ApplicationException($"Can't find booking with id = {bookingId}");

            var flight = Context.Flights.FirstOrDefault(f => f.FlightId == booking.FlightId);

            if (flight != null)
                flight.TotalPlaces += 1;

            DbSet.Remove(booking);

            Context.SaveChanges();

            return booking;
        }

        public IEnumerable<Booking> GetUserBookings(int? userId)
        {
            var bookings = DbSet
                .Include(t => t.Flight)
                .Include(t => t.Flight.Airline)
                .Include(t => t.Flight.Aircraft)
                .Include(t => t.Flight.DepAirport)
                .Include(t => t.Flight.ArrAirport)
                .Include(t => t.User).ToList();

            foreach (var b in bookings)
                if ((DateTime.Now.Day - b.BookingDateTime.Day > 3 && DateTime.Now.Month == b.BookingDateTime.Month) || DateTime.Now.Month > b.BookingDateTime.Month)
                    b.IsActive = false;
                else
                    b.IsActive = true;

            if (userId.HasValue)
                bookings = bookings.Where(b => b.UserId == userId).ToList();
            else
                bookings = bookings.Where(b => b.User.Role != Constants.AdminRole).ToList();

            return bookings;
        }

        public bool IsAlreadyBooked(int userId, int flightId, int amount)
        {
            var bookings = DbSet.Where(b => b.UserId == userId && b.FlightId == flightId).ToList();
            return bookings.Count() + amount > 9;
        }

        public void DeleteAllBookings(int userId)
        {
            var list = DbSet
                .Where(b => b.UserId == userId)
                .ToList();

            foreach (var b in list)
                DbSet.Remove(b);
        }

        public Booking GetBooking(int userId, int flightId)
        {
            var booking = DbSet
                .Where(b => b.UserId == userId && b.FlightId == flightId)
                .FirstOrDefault();

            if (booking == null)
                throw new ApplicationException($"Can't find booking user with id = {userId} on flight with id = {flightId}");

            return booking;
        }

        public void IsThereAccess(int userId, int? bookingId)
        {
            var isThereAccess = false;

            if (bookingId.HasValue)
            {
                isThereAccess = DbSet.Any(b => b.BookingId == bookingId && b.UserId == userId);
                if (!isThereAccess)
                    throw new ApplicationException($"User with id = {userId} does not have access to the booking with id = {bookingId}");
            }
            else
            {
                isThereAccess = (userId == Constants.AdminId);
                if (!isThereAccess)
                    throw new ApplicationException($"User with id = {userId} does not have access to work with bookings");
            }
        }
        public string GetOwner(int bookingId)
        {
            var booking = GetBooking(bookingId);
            var passenger = Context.Users.Where(p => p.UserId == booking.UserId).FirstOrDefault();
            return passenger.Email;
        }

        public IEnumerable<Booking> GetAllByFlight(int flightId)
        {
            return DbSet
                .Include(t => t.Flight)
                .Include(b => b.Flight.Aircraft)
                .Include(b => b.Flight.ArrAirport)
                .Include(b => b.Flight.DepAirport)
                .Include(b => b.User)
                .Where(t => t.FlightId == flightId);
        }

        public Booking NewBooking(Booking booking)
        {
            var entity = DbSet.Add(booking);

            var flight = Context.Flights.FirstOrDefault(f => f.FlightId == booking.FlightId);

            if (flight != null)
                flight.TotalPlaces -= 1;

            Context.SaveChanges();

            return entity;
        }

        public void ChangeStatus(int bookingId, bool status)
        {
            var booking = DbSet.FirstOrDefault(b => b.BookingId == bookingId);

            if (booking != null)
            {
                booking.BookingDateTime = DateTime.Now;
                booking.IsActive = status;
            }

            Context.SaveChanges();
        }
    }
}
