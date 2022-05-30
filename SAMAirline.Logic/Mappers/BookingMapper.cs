using SAMAirline.DataProvider.Entities;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Mappers
{
    public class BookingMapper : IMapper<BookingDto, Booking>
    {
        public Booking MapToDalEntity(BookingDto dtoEntity)
        {
            return new Booking
            {
                BookingId = dtoEntity.BookingId,
                UserId = dtoEntity.OwnerId,
                FlightId = dtoEntity.FlightId,
                PassengerName = dtoEntity.PassengerName,
                PassengerSurname = dtoEntity.PassengerSurname,
                PassengerNationality = dtoEntity.PassengerNationality,
                PassengerBirthdate = dtoEntity.PassengerBirthdate,
                PassengerPassport = dtoEntity.PassengerPassport,
                PassportExpire = dtoEntity.PassportExpire,
                BookingDateTime = dtoEntity.BookingDateTime,
                ContactPhoneNumber = dtoEntity.ContactPhoneNumber,
                ContactEmail = dtoEntity.ContactEmail,
                Price = dtoEntity.Price,
                IsActive = dtoEntity.IsActive
            };
        }

        public BookingDto MapToDtoEntity(Booking dalEntity)
        {
            return new BookingDto
            {
                BookingId = dalEntity.BookingId,
                OwnerId = dalEntity.UserId,
                FlightId = dalEntity.FlightId,
                AirlineCode = dalEntity.Flight.Airline.AirlineCodeIATA,
                AircraftCode = dalEntity.Flight.Aircraft.AircraftCodeIATA,
                ArrAirportCode = dalEntity.Flight.ArrAirport.AirportCodeIATA,
                DepAirportCode = dalEntity.Flight.DepAirport.AirportCodeIATA,
                PassengerName = dalEntity.PassengerName,
                PassengerSurname = dalEntity.PassengerSurname,
                PassengerNationality = dalEntity.PassengerNationality,
                PassengerBirthdate = dalEntity.PassengerBirthdate,
                PassengerPassport = dalEntity.PassengerPassport,
                PassportExpire = dalEntity.PassportExpire,
                BookingDateTime = dalEntity.BookingDateTime,
                ContactPhoneNumber = dalEntity.ContactPhoneNumber,
                ContactEmail = dalEntity.ContactEmail,
                Price = dalEntity.Price,
                IsActive = dalEntity.IsActive
            };
        }
    }
}
