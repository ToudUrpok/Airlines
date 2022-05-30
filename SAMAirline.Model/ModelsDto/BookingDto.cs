using System;

namespace SAMAirline.Model.ModelsDto
{
    public class BookingDto
    {
        public int BookingId { get; set; }
        public int OwnerId { get; set; }

        public int FlightId { get; set; }
        public FlightDto Flight { get; set; }

        public string AirlineCode { get; set; }
        public AirlineDto Airline { get; set; }

        public string AircraftCode { get; set; }
        public AircraftDto Aircraft { get; set; }

        public string DepAirportCode { get; set; }
        public AirportDto DepAirport { get; set; }

        public string ArrAirportCode { get; set; }
        public AirportDto ArrAirport { get; set; }

        public string PassengerName { get; set; }
        public string PassengerSurname { get; set; }
        public string PassengerNationality { get; set; }
        public string PassengerBirthdate { get; set; }
        public string PassengerPassport { get; set; }
        public string PassportExpire { get; set; }

        public DateTime BookingDateTime { get; set; }

        public string ContactPhoneNumber { get; set; }
        public string ContactEmail { get; set; }

        public int Price { get; set; }

        public bool IsActive { get; set; }
        public string Message { get;set; }
    }
}
