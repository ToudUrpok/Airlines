using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Model.PaginationModels;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Services
{
    public class FlightService : IFlightService
    {
        private readonly IFlightRepository _flightRepository;
        private readonly IBookingRepository _bookingRepository;
        private readonly IMapper<FlightDto, Flight> _mapper;

        public FlightService(
            IFlightRepository flightRepository,
            IBookingRepository bookingRepository,
            IMapper<FlightDto, Flight> mapper)
        {
            _flightRepository = flightRepository;
            _bookingRepository = bookingRepository;
            _mapper = mapper;
        }

        public void CancelFlight(int FlightId)
        {
            foreach (var t in _bookingRepository.GetAll().Where(t => t.FlightId == FlightId))
                _bookingRepository.CancelBooking(t.BookingId);
            _flightRepository.CancelFlight(FlightId);
        }

        public IEnumerable<FlightDto> GetAll()
        {
            var list = _flightRepository.GetAll();
            var flights = list.Select(f => _mapper.MapToDtoEntity(f)).ToList();

            return flights;
        }

        public FlightDto GetById(int flightId)
        {
            var f = _flightRepository.GetFlight(flightId);
            if (f != null)
                return _mapper.MapToDtoEntity(f);
            return null;
        }

        public FlightPaginationModel GetNeededFlightsOffset(FlightPaginationModel model)
        {
            var defaultFlights = new List<Flight>();
            var twoWayFlights = new List<Flight>();
            var twoWayFlightList = new List<TwoWayFlightDto>();
            var flights1 = new PagedList<Flight>();
            var flights2 = new PagedList<TwoWayFlightDto>();

            defaultFlights = _flightRepository.GetNeededFlights(model.From, model.To, model.DepDate, model.GetNearby, model.DateOrPrice, model.Count).ToList();

            if (defaultFlights.Count() == 0)
                model.NoResult = true;
            else
                model.NoResult = false;

            if (model.IsTwoWay)
            {
                twoWayFlights = _flightRepository.GetNeededFlights(model.To, model.From, model.ReturnDate, model.GetNearby, model.DateOrPrice, model.Count).ToList();

                foreach (var f1 in defaultFlights)
                {
                    foreach (var f2 in twoWayFlights)
                    {
                        var f1dto = _mapper.MapToDtoEntity(f1);
                        var f2dto = _mapper.MapToDtoEntity(f2);

                        f1dto.DisplayPrice = (f1dto.Price + f2dto.Price) * model.Count;
                        f2dto.DisplayPrice = (f1dto.Price + f2dto.Price) * model.Count;
                        if (f1dto.ArrivalDateTime < f2dto.DepartureDateTime)
                            twoWayFlightList.Add(new TwoWayFlightDto
                            {
                                FirstFlight = f1dto,
                                SecondFlight = f2dto
                            });
                    }
                }

                var count = twoWayFlightList.Count();
                var pagesCount = 0;
                string message = null;

                if (count != 0)
                    if (count < model.FlightList.PageSize)
                        pagesCount = 1;
                    else if (count % model.FlightList.PageSize == 0)
                        pagesCount = count / model.FlightList.PageSize;
                    else
                        pagesCount = count / model.FlightList.PageSize + 1;
                else
                    message = "There are no items";

                model.TwoWayFlightList = new PagedList<TwoWayFlightDto>
                {
                    RowsCount = twoWayFlightList.Count(),
                    PageNumber = model.FlightList.PageNumber,
                    PagesCount = pagesCount,
                    PageSize = model.FlightList.PageSize,
                    Data = twoWayFlightList.Take(model.FlightList.PageNumber * model.FlightList.PageSize).ToList(),
                    Message = message
                };
            }

            flights1 = _flightRepository.GetPagedList(defaultFlights, model.FlightList.PageNumber, model.FlightList.PageSize, model.IsAdmin);
            var flightList = new PagedList<FlightDto>
            {
                RowsCount = flights1.RowsCount,
                PagesCount = flights1.PagesCount,
                PageSize = flights1.PageSize,
                PageNumber = flights1.PageNumber,
                Data = flights1.Data.Select(f => _mapper.MapToDtoEntity(f)).ToList(),
                Message = flights1.Message
            };

            foreach (var f in flightList.Data)
                f.DisplayPrice = f.Price * model.Count;

            model.FlightList = flightList;

            return model;
        }

        public PaginationModel GetFlightsOffset(PaginationModel model)
        {
            var list = _flightRepository.GetNeededFlights(null, null, null, false, true, 0);
            var fl = _flightRepository.GetPagedList(list, model.FlightList.PageNumber, model.FlightList.PageSize, model.IsAdmin);

            var flightList = new PagedList<FlightDto>
            {
                RowsCount = fl.RowsCount,
                PagesCount = fl.PagesCount,
                PageSize = fl.PageSize,
                PageNumber = fl.PageNumber,
                Data = fl.Data.Select(f => _mapper.MapToDtoEntity(f)).ToList(),
                Message = fl.Message
            };
            model.FlightList = flightList;
            return model;
        }

        public FlightDto NewFlight(NewFlightViewModel model)
        {
            return _mapper.MapToDtoEntity(_flightRepository.NewFlight(
                model.Airline,
                model.DepartureDate,
                model.DepartureTime,
                model.DepartureAirport,
                model.Aircraft,
                model.ArrivalDate,
                model.ArrivalTime,
                model.ArrivalAirport,
                model.Stops,
                model.Price
            ));
        }

        public bool IsExists(int flightId)
        {
            return _flightRepository.IsExists(flightId);
        }

        public void IsThereAccess(int userId)
        {
            _flightRepository.IsThereAccess(userId);
        }

        public void GhangeFlightStatus(int flightId, int delay)
        {
            _flightRepository.GhangeFlightStatus(flightId, delay);
        }

        public async Task BulkInsertFlights(IEnumerable<Flight> flights)
        {
            await _flightRepository.BulkInsertFlights(flights);
        }
    }
}
