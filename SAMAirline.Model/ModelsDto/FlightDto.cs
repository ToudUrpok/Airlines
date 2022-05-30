using System;
using System.ComponentModel.DataAnnotations;

namespace SAMAirline.Model.ModelsDto
{
    public class FlightDto
    {
        public int FlightId { get; set; }

        public string FlightNumber { get; set; }

        public string AirlineCode { get; set; }
        public string AirlineName { get; set; }

        public DateTime DepartureDateTime { get; set; }
        public string DepAirportCode { get; set; }
        public string DepAirportName { get; set; }
        public string DepartureCity { get; set; }
        public string DepartureCountry { get; set; }

        public string AircraftCode { get; set; }
        public string AircraftName { get; set; }

        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "IncorrectPlaces")]
        public int FreePlaces { get; set; }

        public DateTime ArrivalDateTime { get; set; }
        public string ArrAirportCode { get; set; }
        public string ArrAirportName { get; set; }
        public string ArrivalCity { get; set; }
        public string ArrivalCountry { get; set; }

        public int Stops { get; set; }

        [RegularExpression(@"[0-9]+", ErrorMessageResourceType = typeof(Resources.Resource),ErrorMessageResourceName = "IncorrectPrice")]
        public int Price { get; set; }

        public int DisplayPrice { get; set; }
    }
}
