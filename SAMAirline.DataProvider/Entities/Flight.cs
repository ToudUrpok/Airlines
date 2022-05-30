namespace SAMAirline.DataProvider.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Flight")]
    public partial class Flight
    {
        [Key]
        public int FlightId { get; set; }

        [Required]
        [StringLength(15)]
        public string FlightNumber { get; set; }

        [Required]
        [StringLength(4)]
        public string AirlineCode { get; set; }

        [Required]
        public int AirlineId { get; set; }
        public virtual Airline Airline { get; set; }

        [Required]
        public DateTime DepartureDateTime { get; set; }

        [Required]
        [StringLength(3)]
        public string DepartureAirportCode { get; set; }
        [Required]
        public int DepartureAirportId { get; set; }
        public virtual Airport DepAirport { get; set; }

        [Required]
        [StringLength(3)]
        public string AircraftCode { get; set; }
        [Required]
        public int AircraftId { get; set; }
        public virtual Aircraft Aircraft { get; set; }

        public int TotalPlaces { get; set; }

        [Required]
        public DateTime ArrivalDateTime { get; set; }

        [Required]
        [StringLength(3)]
        public string ArrivalAirportCode { get; set; }
        [Required]
        public int ArrivalAirportId { get; set; }
        public virtual Airport ArrAirport { get; set; }

        [Required]
        public int Stops { get; set; }

        [Required]
        public int Price { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        public Flight()
        {
        }

        public Flight(
            int flightId, string flightNumber, string airlineCode, int airlineId, 
            DateTime departureDateTime, int departureAirportId, string departureAirportCode, 
            string aircraftCode, int aircraftId, int totalPlaces,
            DateTime arrivalDateTime, int arrivalAirportId, string arrivalAirportCode,
            int stops, int price)
        {
            FlightId = flightId;
            FlightNumber = flightNumber;
            AirlineCode = airlineCode;
            AirlineId = airlineId;
            DepartureDateTime = departureDateTime;
            DepartureAirportId = departureAirportId;
            DepartureAirportCode = departureAirportCode;
            AircraftId = aircraftId;
            AircraftCode = aircraftCode;
            TotalPlaces = totalPlaces;
            ArrivalDateTime = arrivalDateTime;
            ArrivalAirportId = arrivalAirportId;
            ArrivalAirportCode = arrivalAirportCode;
            Stops = stops;
            Price = price;
        }
    }
}
