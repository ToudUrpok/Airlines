using SAMAirline.Common;
using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace SAMAirline.DataProvider.Repositories
{
    public class FlightRepository : BaseRepository<Flight>, IFlightRepository
    {
        public FlightRepository(AirlineDB context) : base(context)
        {
        }

        public void CancelFlight(int flightId)
        {
            var flight = DbSet
                .Where(f => f.FlightId == flightId)
                .FirstOrDefault();

            if (flight == null)
                throw new ApplicationException($"Can't find flight with id = {flightId}");

            DbSet.Remove(flight);
        }

        public IEnumerable<Flight> GetFlights()
        {
            return DbSet
                .Include(f => f.Aircraft)
                .Include(f => f.DepAirport)
                .Include(f => f.ArrAirport);
        }


        public Flight GetFlight(int flightId)
        {
            var flight = DbSet
                .Include(f => f.Aircraft)
                .Include(f => f.DepAirport)
                .Include(f => f.ArrAirport)
                .Where(f => f.FlightId == flightId)
                .FirstOrDefault();

            return flight;
        }


        public int GetFlightsCount(string from, string to, DateTime? date)
        {
            var flights = DbSet
                .Include(f => f.Aircraft)
                .Include(f => f.DepAirport)
                .Include(f => f.ArrAirport);

            if (from != null)
                flights = flights
                    .Where(
                    f => f.DepAirport.City.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.AirportCodeIATA.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.AirportCodeICAO.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.Country.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.City.ToLower().Contains(from.ToLower()));

            if (to != null)
                flights = flights
                    .Where(
                    f => f.ArrAirport.City.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.AirportCodeIATA.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.AirportCodeICAO.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.Country.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.City.ToLower().Contains(from.ToLower()));

            if (date.HasValue)
                flights = flights.Where(f =>
                    f.DepartureDateTime.Day == date.Value.Day &&
                    f.DepartureDateTime.Month == date.Value.Month &&
                    f.DepartureDateTime.Year == date.Value.Year);

            return flights.Count();
        }

        public IEnumerable<Flight> GetFlightsOffset(string from, string to, DateTime? date, bool orderAscending, bool sortByDate, int pageNumber, int pageSize)
        {
            var flights = DbSet
                .Include(f => f.Aircraft)
                .Include(f => f.DepAirport)
                .Include(f => f.ArrAirport);

            if (from != null)
                flights = flights
                    .Where(
                    f => f.DepAirport.City.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.AirportCodeIATA.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.AirportCodeICAO.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.Country.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.City.ToLower().Contains(from.ToLower()));

            if (to != null)
                flights = flights
                    .Where(
                    f => f.ArrAirport.City.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.AirportCodeIATA.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.AirportCodeICAO.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.Country.ToLower().Contains(from.ToLower()) ||
                    f.DepAirport.City.ToLower().Contains(from.ToLower()));

            if (date.HasValue)
                flights = flights.Where(f =>
                    f.DepartureDateTime.Day == date.Value.Day &&
                    f.DepartureDateTime.Month == date.Value.Month &&
                    f.DepartureDateTime.Year == date.Value.Year);

            if (sortByDate)
                if (orderAscending)
                    flights = flights.OrderBy(f => f.DepartureDateTime);
                else
                    flights = flights.OrderByDescending(f => f.DepartureDateTime);
            else
                if (orderAscending)
                flights = flights.OrderBy(f => f.Price);
            else
                flights = flights.OrderByDescending(f => f.Price);

            flights = flights
                .Take(pageNumber * pageSize);

            return flights;
        }

        public IEnumerable<Flight> GetNeededFlights(string from, string to, DateTime? depDate, bool getNearby, bool dateOrPrice, int count)
        {
            var depAirport = Context.Airports.FirstOrDefault(a =>
                    from.ToLower().Contains(a.AirportCodeIATA.ToLower()) &&
                    from.ToLower().Contains(a.Country.ToLower()) &&
                    from.ToLower().Contains(a.City.ToLower()));

            var arrAirport = Context.Airports.FirstOrDefault(a =>
                    to.ToLower().Contains(a.AirportCodeIATA.ToLower()) &&
                    to.ToLower().Contains(a.Country.ToLower()) &&
                    to.ToLower().Contains(a.City.ToLower()));

            var flights = (IEnumerable<Flight>)DbSet
                .Include(f => f.Aircraft)
                .Include(f => f.Airline)
                .Include(f => f.DepAirport)
                .Include(f => f.ArrAirport);

            if (from != null && depAirport != null)
                flights = flights
                    .Where(f => f.DepartureAirportId == depAirport.AirportId);

            if (to != null && arrAirport != null)
                flights = flights
                    .Where(f => f.ArrivalAirportId == arrAirport.AirportId);

            if (!getNearby && depDate.HasValue)
                flights = flights.Where(f =>
                    f.DepartureDateTime.Day == depDate.Value.Day &&
                    f.DepartureDateTime.Month == depDate.Value.Month &&
                    f.DepartureDateTime.Year == depDate.Value.Year);

            if (getNearby && depDate.HasValue)
            {
                var flightsBefore = flights.Where(f =>
                    f.DepartureDateTime.Day < depDate.Value.Day || f.DepartureDateTime.Month < depDate.Value.Month);
                flightsBefore = flightsBefore.Skip(Math.Max(0, flightsBefore.Count() - 4));
                var flightsAfter = flights.Where(f =>
                    f.DepartureDateTime.Day >= depDate.Value.Day || f.DepartureDateTime.Month >= depDate.Value.Month).Take(4);
                flights = flightsBefore.Concat(flightsAfter);
            }

            if (count != 0)
                flights = flights.Where(f => f.TotalPlaces >= count);

            if (dateOrPrice)
                flights = flights.OrderBy(f => f.DepartureDateTime);
            else
                flights = flights.OrderBy(f => f.Price);

            return flights;
        }

        public Flight NewFlight(string airlineCode, DateTime departureDate, string departureTime, string departureAirport, string aircraftCode, DateTime arrivalDate, string arrivalTime, string arrivalAirport, int stops, int price)
        {
            var airline = Context.Airlines.FirstOrDefault(a => (airlineCode.Contains(a.Name) || airlineCode.Contains(a.AirlineCodeIATA)) && airlineCode.Contains(a.Country));
            var aircraft = Context.Aircrafts.FirstOrDefault(a => aircraftCode.Contains(a.AircraftCodeIATA));
            var depAirport = Context.Airports.FirstOrDefault(a => (departureAirport.Contains(a.Name) || departureAirport.Contains(a.AirportCodeIATA)) && departureAirport.Contains(a.City));
            var arrAirport = Context.Airports.FirstOrDefault(a => (arrivalAirport.Contains(a.Name) || arrivalAirport.Contains(a.AirportCodeIATA)) && arrivalAirport.Contains(a.City));

            if (airline != null && aircraft != null && depAirport != null && arrAirport != null)
            {
                var flight = new Flight()
                {
                    FlightNumber = $"{airline.AirlineCodeIATA}-{depAirport.AirportCodeIATA}-{arrAirport.AirportCodeIATA}",
                    AirlineId = airline.AirlineId,
                    AirlineCode = airline.AirlineCodeIATA,
                    DepartureDateTime = new DateTime(departureDate.Year, departureDate.Month, departureDate.Day, Convert.ToInt32(departureTime.Split(new char[] { ':' })[0]), Convert.ToInt32(departureTime.Split(new char[] { ':' })[1]), 0),
                    DepartureAirportId = depAirport.AirportId,
                    DepartureAirportCode = depAirport.AirportCodeIATA,
                    AircraftId = aircraft.AircraftId,
                    AircraftCode = aircraft.AircraftCodeIATA,
                    TotalPlaces = aircraft.TotalPlaces,
                    ArrivalDateTime = new DateTime(arrivalDate.Year, arrivalDate.Month, arrivalDate.Day, Convert.ToInt32(arrivalTime.Split(new char[] { ':' })[0]), Convert.ToInt32(arrivalTime.Split(new char[] { ':' })[1]), 0),
                    ArrivalAirportId = arrAirport.AirportId,
                    ArrivalAirportCode = arrAirport.AirportCodeIATA,
                    Stops = stops,
                    Price = price
                };

                DbSet.Add(flight);
                Context.SaveChanges();
                return flight;
            }

            return null;
        }

        public void IsThereAccess(int userId)
        {
            if (userId != Constants.AdminId)
                throw new ApplicationException($"User with id = {userId} does not have access to work with flights");
        }

        public int GetIntentPlaces(int flightId)
        {
            int x = 0;
            var flight = GetFlight(flightId);

            var bookings = Context.Bookings.Where(b => b.FlightId == flightId && b.IsActive == true);

            return bookings.Count();
        }

        public void GhangeFlightStatus(int flightId, int delay)
        {
            var flight = DbSet
               .Where(f => f.FlightId == flightId)
               .FirstOrDefault();

            if (flight == null)
                throw new ApplicationException($"Can't find flight with id = {flightId}");

            flight.DepartureDateTime = flight.DepartureDateTime.AddHours(delay);
            flight.ArrivalDateTime = flight.ArrivalDateTime.AddHours(delay);

            Context.SaveChanges();
        }

        public async Task BulkInsertFlights(IEnumerable<Flight> flights)
        {
            DbSet.AddRange(flights);

            await Context.SaveChangesAsync();
        }
    }
}
