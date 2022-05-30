using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.DataProvider.Repositories;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Model.PaginationModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<BookingDto, Booking> _mapperBooking;
        private readonly IMapper<AirlineDto, Airline> _mapperAirline;
        private readonly IMapper<AircraftDto, Aircraft> _mapperAircraft;
        private readonly IMapper<AirportDto, Airport> _mapperAirport;
        private readonly IMapper<FlightDto, Flight> _mapperFlight;

        public BookingService(
            IBookingRepository bookingRepository,
            IMapper<BookingDto, Booking> mapperBooking,
            IMapper<AirlineDto, Airline> mapperAirline,
            IMapper<AircraftDto, Aircraft> mapperAircraft,
            IMapper<AirportDto, Airport> mapperAirport,
            IMapper<FlightDto, Flight> mapperFlight)
        {
            _bookingRepository = bookingRepository;
            _mapperBooking = mapperBooking;
            _mapperAirline = mapperAirline;
            _mapperAircraft = mapperAircraft;
            _mapperAirport = mapperAirport;
            _mapperFlight = mapperFlight;
        }

        public IEnumerable<BookingDto> GetAll()
        {
            var list = _bookingRepository.GetDefaultUsersBookings();

            var bookings = list.Select(b => _mapperBooking.MapToDtoEntity(b)).ToList();

            if (bookings.Count() == 0)
                throw new ApplicationException("There are no active bookings");

            return bookings;
        }

        public bool IsExists(int id)
        {
            return _bookingRepository.IsExists(id);
        }

        public BookingDto GetById(int bookingId)
        {
            var b = _bookingRepository.GetBooking(bookingId);

            if (b == null)
                throw new NullReferenceException($"Can't find booking with id = {bookingId}");

            var booking = _mapperBooking.MapToDtoEntity(b);

            return booking;
        }

        public void CancelBooking(int bookingId)
        {
            _bookingRepository.CancelBooking(bookingId);
        }

        public IEnumerable<BookingDto> GetUsersBooking(int userId)
        {
            var list = _bookingRepository.GetUserBookings(userId);

            var usersBookings = list.Select(b => _mapperBooking.MapToDtoEntity(b)).ToList();

            return usersBookings;
        }

        public void DeleteAllBookings(int userId)
        {
            _bookingRepository.DeleteAllBookings(userId);
        }

        public bool IsAlreadyBooked(int userId, int flightId, int amount)
        {
            return _bookingRepository.IsAlreadyBooked(userId, flightId, amount);
        }

        public PaginationModel GetBookingsOffset(PaginationModel model)
        {
            var list = _bookingRepository.GetUserBookings(model.UserId);
            var bl = _bookingRepository.GetPagedList(list, model.BookingList.PageNumber, model.BookingList.PageSize, model.IsAdmin);

            var bookingList = new PagedList<BookingDto>
            {
                RowsCount = bl.RowsCount,
                PagesCount = bl.PagesCount,
                PageSize = bl.PageSize,
                PageNumber = bl.PageNumber,
                Message = bl.Message,
                Data = new List<BookingDto>()
            };

            foreach(var b in bl.Data)
            {
                var bookingDto = _mapperBooking.MapToDtoEntity(b);

                bookingDto.Flight = _mapperFlight.MapToDtoEntity(b.Flight);
                bookingDto.Airline = _mapperAirline.MapToDtoEntity(b.Flight.Airline);
                bookingDto.Aircraft = _mapperAircraft.MapToDtoEntity(b.Flight.Aircraft);
                bookingDto.DepAirport = _mapperAirport.MapToDtoEntity(b.Flight.DepAirport);
                bookingDto.ArrAirport = _mapperAirport.MapToDtoEntity(b.Flight.ArrAirport);

                bookingList.Data.Add(bookingDto);
            }

            model.BookingList = bookingList;
            return model;
        }

        public void IsThereAccess(int userId, int? bookingId)
        {
            _bookingRepository.IsThereAccess(userId, bookingId);
        }

        public string GetOwner(int bookingId)
        {
            return _bookingRepository.GetOwner(bookingId);
        }
        public IEnumerable<BookingDto> GetAllByFlight(int flightId)
        {
            var list = _bookingRepository.GetAllByFlight(flightId);
            var bookings = list.Select(b => _mapperBooking.MapToDtoEntity(b)).ToList();
            return bookings;
        }
        public void NewBooking(int userId, NewBookingViewModel model)
        {
            foreach (var person in model.PassengerInfos)
            {
                _bookingRepository.NewBooking(new Booking
                {
                    FlightId = model.FlightId,
                    UserId = userId,
                    PassengerName = person.PassengerName,
                    PassengerSurname = person.PassengerSurname,
                    PassengerNationality = person.PassengerNationality,
                    PassengerBirthdate = person.PassengerBirthdate,
                    PassengerPassport = person.PassengerPassport,
                    PassportExpire = person.PassportExpire,
                    BookingDateTime = DateTime.Now,
                    ContactPhoneNumber = model.ContactPhoneNumber,
                    ContactEmail = model.ContactEmail,
                    Price = (int)(model.Flight.Price * 1.1),
                    IsActive = true
                });
                if (model.SecondFlightId != 0)
                {
                    _bookingRepository.NewBooking(new Booking
                    {
                        FlightId = model.SecondFlightId,
                        UserId = userId,
                        PassengerName = person.PassengerName,
                        PassengerSurname = person.PassengerSurname,
                        PassengerNationality = person.PassengerBirthdate,
                        PassengerBirthdate = person.PassengerBirthdate,
                        PassengerPassport = person.PassengerPassport,
                        PassportExpire = person.PassportExpire,
                        BookingDateTime = DateTime.Now,
                        ContactPhoneNumber = model.ContactPhoneNumber,
                        ContactEmail = model.ContactEmail,
                        Price = (int)(model.SecondFlight.Price * 1.1),
                        IsActive = true
                    });
                }
            }
        }

        public void ChangeStatus(int bookingId, bool status)
        {
            _bookingRepository.ChangeStatus(bookingId, status);
        }
    }
}
