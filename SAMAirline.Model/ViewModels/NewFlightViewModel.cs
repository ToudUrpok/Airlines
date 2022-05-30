using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class NewFlightViewModel 
    {
        [Required]
        public string Airline { get; set; }

        [Required]
        public DateTime DepartureDate { get; set; }

        [Required]
        public string DepartureTime { get; set; }

        [Required]
        public string DepartureAirport { get; set; }

        [Required]
        public string Aircraft { get; set; }

        [Required]
        public DateTime ArrivalDate { get; set; }

        [Required]
        public string ArrivalTime { get; set; }

        [Required]
        public string ArrivalAirport { get; set; }

        [Required]
        [RegularExpression(@"[0-3]", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectStops")]
        public int Stops { get; set; }

        [Required]
        [RegularExpression(@"[0-9]+", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectPrice")]
        public int Price { get; set; }
    }
}
