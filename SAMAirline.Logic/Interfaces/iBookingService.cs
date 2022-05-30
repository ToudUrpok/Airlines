using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Model.PaginationModels;
using System.Collections.Generic;

namespace SAMAirline.Logic.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingDto> GetAll();
        IEnumerable<BookingDto> GetUsersBooking(int userId);
        IEnumerable<BookingDto> GetAllByFlight(int flightId);
        bool IsExists(int id);
        BookingDto GetById(int bookingId);
        bool IsAlreadyBooked(int userId, int flightId, int amount);
        void NewBooking(int userId, NewBookingViewModel model);
        void CancelBooking(int bookingId);
        void DeleteAllBookings(int userId);
        PaginationModel GetBookingsOffset(PaginationModel model);
        void IsThereAccess(int userId, int? bookingId);
        void ChangeStatus(int bookingId, bool status);
        string GetOwner(int bookingId);
    }
}
