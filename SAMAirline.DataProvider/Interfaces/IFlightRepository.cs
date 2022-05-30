using SAMAirline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SAMAirline.DataProvider.Interfaces
{
    public interface IFlightRepository : IRepository<Flight>
    {
        IEnumerable<Flight> GetFlights();
        Flight GetFlight(int flightId);
        int GetFlightsCount(string from, string to, DateTime? date);
        IEnumerable<Flight> GetNeededFlights(string from, string to, DateTime? depDate, bool getNearby, bool dateOrPrice, int count);
        Flight NewFlight(string airlineCode, DateTime departureDate, string departureTime, string departureAirport, string aircraftCode, DateTime arrivalDate, string arrivalTime, string arrivalAirport, int stops, int price);
        void CancelFlight(int flightId);
        void IsThereAccess(int userId);
        int GetIntentPlaces(int flightId);
        void GhangeFlightStatus(int flightId, int delay);
        Task BulkInsertFlights(IEnumerable<Flight> flights);
    }
}
