using SAMAirline.DataProvider.Entities;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Model.PaginationModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Interfaces
{
    public interface IFlightService
    {
        IEnumerable<FlightDto> GetAll();
        FlightDto GetById(int flightId);
        FlightPaginationModel GetNeededFlightsOffset(FlightPaginationModel model);
        PaginationModel GetFlightsOffset(PaginationModel model);
        FlightDto NewFlight(NewFlightViewModel model);
        Task BulkInsertFlights(IEnumerable<Flight> flights);
        void CancelFlight(int flightId);
        bool IsExists(int flightId);
        void IsThereAccess(int userId);
        void GhangeFlightStatus(int flightId, int delay);
    }
}
