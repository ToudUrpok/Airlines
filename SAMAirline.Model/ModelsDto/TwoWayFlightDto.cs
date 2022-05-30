using SAMAirline.Model.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.ModelsDto
{
    public class TwoWayFlightDto
    {
        public FlightDto FirstFlight { get; set; }
        public FlightDto SecondFlight { get; set; }
    }
}
