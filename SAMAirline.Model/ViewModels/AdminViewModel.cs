using SAMAirline.Model.ModelsDto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class AdminViewModel : NewFlightViewModel
    {
        public IEnumerable<FlightDto> FlightsList { get; set; }
        public IEnumerable<BookingDto> BookingsList { get; set; }
        public IEnumerable<UserDto> UsersList { get; set; }
        public IEnumerable<AirportDto> AirportsList { get; set; }
        public IEnumerable<AircraftDto> AircraftsList { get; set; }
    }
}
