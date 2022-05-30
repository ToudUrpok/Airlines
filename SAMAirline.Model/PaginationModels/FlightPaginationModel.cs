using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System;

namespace SAMAirline.Model.PaginationModels
{
    public class FlightPaginationModel
    {
        public string From { get; set; }

        public string To { get; set; }

        public DateTime? DepDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public int Count { get; set; }

        public bool IsTwoWay { get; set; } = false;

        public bool GetNearby { get; set; }

        public bool DateOrPrice { get; set; } = true;

        public int PageNumber { get; set; } = 1;

        public bool NoResult { get; set; } = false;

        public PagedList<FlightDto> FlightList { get; set; }

        public PagedList<TwoWayFlightDto> TwoWayFlightList { get; set; }

        public bool IsAdmin { get; set; } = false;

        public FlightPaginationModel()
        {

        }
    }
}
