using SAMAirline.Model.ModelsDto;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class PagedList<T>
    {
        public int RowsCount { get; set; }
        public int PagesCount { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; } = 1;
        public List<T> Data { get; set; }
        public string Message { get; set; }
    }

    public class PaginationModel : NewFlightViewModel
    {
        public int? UserId { get; set; }
        public PagedList<BookingDto> BookingList { get; set; }
        public PagedList<UserDto> UserList { get; set; }
        public PagedList<FlightDto> FlightList { get; set; }
        public IEnumerable AirportList { get; set; }
        public IEnumerable AircraftList { get; set; }
        public bool IsAdmin { get; set; } = false;

        public PaginationModel()
        {

        }
    }
}