using Newtonsoft.Json;
using SAMAirline.Common;
using SAMAirline.DataProvider.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.Hosting;

namespace SAMAirline.DataProvider
{
    public class AirlineDBInitializer : CreateDatabaseIfNotExists<AirlineDB>
    {
        protected override void Seed(AirlineDB context)
        {
            Random random = new Random();

            Regex rgx = new Regex(@"^[a-zA-Z0-9]+$");

            IList<Airline> defaultAirlines = new List<Airline>();
            using (StreamReader r = new StreamReader(HostingEnvironment.MapPath("~/Data/airlines.json")))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var airline in array)
                {
                    if (rgx.IsMatch(Convert.ToString(airline.iata)) &&
                        rgx.IsMatch(Convert.ToString(airline.icao)) &&
                        airline.country != null)
                        defaultAirlines.Add(
                            new Airline(
                                Convert.ToInt32(airline.id), Convert.ToString(airline.iata), Convert.ToString(airline.icao),
                                Convert.ToString(airline.name), Convert.ToString(airline.country)));
                }
            }
            context.Airlines.AddRange(defaultAirlines);

            context.SaveChanges();

            IList<Airport> defaultAirports = new List<Airport>();
            using (StreamReader r = new StreamReader(HostingEnvironment.MapPath("~/Data/airports.json")))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var airport in array)
                {
                    if (rgx.IsMatch(Convert.ToString(airport.iata)) &&
                        rgx.IsMatch(Convert.ToString(airport.icao)) &&
                        airport.name != null && airport.country != null &&
                        !string.IsNullOrEmpty(Convert.ToString(airport.city)))
                        defaultAirports.Add(
                            new Airport(
                                Convert.ToInt32(airport.id), Convert.ToString(airport.iata),
                                Convert.ToString(airport.icao), Convert.ToString(airport.name),
                                Convert.ToString(airport.country), Convert.ToString(airport.city)));
                }
            }
            context.Airports.AddRange(defaultAirports);

            context.SaveChanges();

            int aircraftCounter = 0;
            IList<Aircraft> defaultAircrafts = new List<Aircraft>();
            using (StreamReader r = new StreamReader(HostingEnvironment.MapPath("~/Data/aircrafts.json")))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var aircraft in array)
                {
                    int totalPlaces = random.Next(150, 500);

                    if (rgx.IsMatch(Convert.ToString(aircraft.iata)) &&
                        rgx.IsMatch(Convert.ToString(aircraft.icao)) &&
                        aircraft.name != null && !string.IsNullOrEmpty(Convert.ToString(aircraft.name)))
                        defaultAircrafts.Add(
                            new Aircraft(
                                ++aircraftCounter, Convert.ToString(aircraft.iata), Convert.ToString(aircraft.icao),
                                Convert.ToString(aircraft.name), totalPlaces - totalPlaces % 10));
                }
            }
            context.Aircrafts.AddRange(defaultAircrafts);

            context.SaveChanges();

            #region OldFlights
            //IList<Flight> defaultFlights = new List<Flight>();

            ////for (int j = 0; j < defaultAircrafts.Count(); j++)
            ////{
            ////    for (int i = 0; i < defaultAirports.Count(); i++)
            ////    {
            ////        int month = random.Next(4, 10);
            ////        int depDay = random.Next(1, 29);
            ////        int flightTime = random.Next(5, 12);
            ////        int price = flightTime * 800 / 5;
            ////        int price2 = flightTime * 800 / 7;
            ////        int arrDay;
            ////        int depHour = random.Next(0, 23);
            ////        int arrHour = depHour + flightTime;
            ////        int m = i + 1;
            ////        if (arrHour > 23)
            ////        {
            ////            arrHour = arrHour - 23;
            ////            arrDay = depDay + 1;
            ////        }
            ////        else
            ////            arrDay = depDay;

            ////        if (i == defaultAirports.Count() - 1)
            ////            m = 0;
            ////        int depMinute = random.Next(1, 3);
            ////        int arrMinute = random.Next(1, 3);

            ////        defaultFlights.Add(new Flight()
            ////        {
            ////            FlightNumber = airlineCompanies[random.Next(1, airlineCompanies.Count())] + Convert.ToString(counter).Substring(1),
            ////            DepartureDateTime = new DateTime(2020, month, depDay, depHour, minutes[depMinute], 00),
            ////            DepartureAirport = defaultAirports[i].AirportCode,
            ////            AircraftCode = defaultAircrafts[j].AircraftCode,
            ////            TotalPlaces = defaultAircrafts[j].TotalPlaces,
            ////            ArrivalDateTime = new DateTime(2020, month, arrDay, arrHour, minutes[arrMinute], 00),
            ////            ArrivalAirport = defaultAirports[m].AirportCode,
            ////            Price = price
            ////        });
            ////        defaultFlights.Add(new Flight()
            ////        {
            ////            FlightNumber = airlineCompanies[random.Next(1, airlineCompanies.Count())] + Convert.ToString(counter + 1).Substring(1),
            ////            DepartureDateTime = new DateTime(2020, month, depDay + 1, depHour / 2 + random.Next(depHour / 4, depHour / 2), minutes[depMinute], 00),
            ////            DepartureAirport = defaultAirports[m].AirportCode,
            ////            AircraftCode = defaultAircrafts[j].AircraftCode,
            ////            TotalPlaces = defaultAircrafts[j].TotalPlaces,
            ////            ArrivalDateTime = new DateTime(2020, month, arrDay + 1, arrHour / 2 + random.Next(arrHour / 4, arrHour / 2), minutes[arrMinute], 00),
            ////            ArrivalAirport = defaultAirports[i].AirportCode,
            ////            Price = price2
            ////        });
            ////        counter++;
            ////    }
            ////}
            #endregion
            #region NewFlights
            //int flightCounter = 0;
            //using (StreamReader r = new StreamReader(HostingEnvironment.MapPath("~/Data/flights.json")))
            //{
            //    string json = r.ReadToEnd();
            //    dynamic array = JsonConvert.DeserializeObject(json);
            //    foreach (var flight in array)
            //    {
            //        if (flight.sourceAirportId != null &&
            //            flight.destinationAirportId != null &&
            //            flight.airlineId != null)
            //        {
            //            var airline = defaultAirlines.Where(a => a.AirlineId == Convert.ToInt32(flight.airlineId) || Convert.ToString(flight.airlineCode).Contains(a.AirlineCodeIATA) || Convert.ToString(flight.airlineCode).Contains(a.AirlineCodeICAO)).FirstOrDefault();
            //            var aircraft = defaultAircrafts.Where(a => Convert.ToString(flight.aircraftIata).Contains(a.AircraftCodeIATA) || Convert.ToString(flight.aircraftIata).Contains(a.AircraftCodeICAO)).FirstOrDefault();
            //            var sourceAirport = defaultAirports.Where(a => a.AirportId == Convert.ToInt32(flight.sourceAirportId)).FirstOrDefault();
            //            var destAirport = defaultAirports.Where(a => a.AirportId == Convert.ToInt32(flight.destinationAirportId)).FirstOrDefault();

            //            if (airline != null && aircraft != null && sourceAirport != null && destAirport != null)
            //            {
            //                int flightTime = random.Next(5, 12);

            //                for (int i = 0; i < 10; i++)
            //                {
            //                    int[] minutes = { 0, 15, 30, 45 };
            //                    int month = random.Next(5, 8);
            //                    int depDay = random.Next(1, 29);
            //                    int tempPrice = random.Next(flightTime * 800 / 5 - 100, flightTime * 800 / 5 + 100);
            //                    int price = tempPrice - tempPrice % 10;
            //                    int arrDay;
            //                    int depHour = random.Next(0, 23);
            //                    int arrHour = depHour + flightTime;
            //                    if (arrHour > 23)
            //                    {
            //                        arrHour = arrHour - 23;
            //                        arrDay = depDay + 1;
            //                    }
            //                    else
            //                        arrDay = depDay;

            //                    int depMinute = random.Next(1, 3);
            //                    int arrMinute = random.Next(1, 3);

            //                    defaultFlights.Add(
            //                        new Flight(
            //                            ++flightCounter,
            //                            $"{airline.AirlineCodeIATA}-{sourceAirport.AirportCodeIATA}-{destAirport.AirportCodeIATA}",
            //                            airline.AirlineCodeIATA, airline.AirlineId, new DateTime(2020, month, depDay, depHour, minutes[depMinute], 00),
            //                            sourceAirport.AirportId, sourceAirport.AirportCodeIATA,
            //                            aircraft.AircraftCodeIATA, aircraft.AircraftId, aircraft.TotalPlaces,
            //                            new DateTime(2020, month, arrDay, arrHour, minutes[arrMinute], 00),
            //                            destAirport.AirportId, destAirport.AirportCodeIATA, Convert.ToInt32(flight.stops), price));
            //                }
            //            }
            //        }
            //    }
            //}

            //context.Flights.AddRange(defaultFlights);

            //context.SaveChanges();
            #endregion

            IList<User> defaultUsers = new List<User>();
            defaultUsers.Add(new User() { ProfileImage = null, Name = "admin", Surname = "admin", Email = "admin@admin.com", Password = Crypto.Sha256("admin" + "admin@admin.com"), Role = Constants.AdminRole, IsConfirmed = true });
            defaultUsers.Add(new User() { ProfileImage = null, Name = "Maxim", Surname = "Grechuha", Email = "max.grechuha@mail.ru", Password = Crypto.Sha256("123456" + "max.grechuha@mail.ru"), Role = Constants.UserRole, IsConfirmed = true });
            context.Users.AddRange(defaultUsers);

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
