using Newtonsoft.Json;
using SAMAirline.DataProvider.Entities;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Logic.Services;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Website.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Mvc;

namespace SAMAirline.Website.Controllers
{
    [Culture]
    [ExceptionLogger]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IFlightService _flightService;
        private readonly IBookingService _bookingService;
        private readonly IAirlineService _airlineService;
        private readonly IAircraftService _aircraftService;
        private readonly IAirportService _airportService;
        private readonly INotificationService _notificationService;

        public const int Amount = 5;

        public AdminController(
            IFlightService flightService,
            IBookingService bookingService,
            IAirlineService airlineService,
            IAircraftService aircraftService,
            IAirportService airportService,
            INotificationService notificationService)
        {
            _bookingService = bookingService;
            _flightService = flightService;
            _airlineService = airlineService;
            _aircraftService = aircraftService;
            _airportService = airportService;
            _notificationService = notificationService;
        }

        EmailService emailService = new EmailService();

        public ActionResult Index()
        {
            var model = new PaginationModel();
            return View(model);
        }

        [HttpGet]
        public ActionResult SaveJson()
        {

            var rgx = new Regex(@"[\p{L}-[a-zA-Z\s]]+$");
            const string quote = "\"";

            var airlinesString = new List<String>();
            var aircraftsString = new List<String>();
            var airportsString = new List<String>();

            var airlines = _airlineService.GetAll();
            var aircrafts = _aircraftService.GetAll();
            var airports = _airportService.GetAll();

            foreach (var a in airlines)
            {
                airlinesString.Add($"{quote}{a.AirlineCodeIATA}, {(a.Name.Length > 25 ? a.Name.Substring(0, 25) + "..." : a.Name)}, {a.Country} {quote}");
            }
            foreach (var a in aircrafts)
            {
                aircraftsString.Add($"{quote}{a.AircraftCodeIATA}, {(a.Model.Length > 25 ? a.Model.Substring(0, 25) + "..." : a.Model)} {quote}");
            }
            foreach (var a in airports)
            {
                var name = Regex.Replace(a.Name, @"[^\u0000-\u007F]+", string.Empty);
                var city = Regex.Replace(a.City, @"[^\u0000-\u007F]+", string.Empty);
                var country = Regex.Replace(a.Country, @"[^\u0000-\u007F]+", string.Empty);
                //var name = Regex.Replace(a.Name, @"[^\x00-\x7F]", "");
                //if (rgx.IsMatch(a.Name) && rgx.IsMatch(a.City))
                airportsString.Add($"{quote}{a.AirportCodeIATA}, {(a.Name.Length > 15 ? a.Name.Substring(0, 15) + "..." : a.Name).Replace(quote, "")}, {city}, {country}{quote}");
            }


            string writePath = @"C:\Users\makhr\Desktop\SAMAirline\Source\SAMAirline.Website\Content\sources\source.txt";

            try
            {
                using (StreamWriter sw = new StreamWriter(writePath, true, System.Text.Encoding.Default))
                {
                    sw.WriteLine("Airlines");
                    sw.WriteLine(String.Join(", ", airlinesString));
                    sw.WriteLine("Aircrafts");
                    sw.WriteLine(String.Join(", ", aircraftsString));
                    sw.WriteLine("Airports");
                    sw.WriteLine(String.Join(", ", airportsString));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        [HttpGet]
        public async Task<ActionResult> SendNotificationsAsync()
        {
            EmailService emailService = new EmailService();

            var bookings = _bookingService.GetAll();

            bookings = bookings
                .Where(b => 
                    (DateTime.Now.Day - b.BookingDateTime.Day >= 3 && DateTime.Now.Month == b.BookingDateTime.Month) || 
                    (DateTime.Now.Month > b.BookingDateTime.Month));

            _notificationService.NewNotifications(bookings);

            foreach (var b in bookings)
            {
                await emailService.SendBookingNotificationAsync(_bookingService.GetOwner(b.BookingId), b);
            }

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
               
        [HttpGet]
        public async Task<ActionResult> AddFlights()
        {
            Random random = new Random();

            var defaultAirlines = _airlineService.GetAll();
            var defaultAircrafts = _aircraftService.GetAll();
            var defaultAirports = _airportService.GetAll();
            IList<Flight> defaultFlights = new List<Flight>();

            int flightCounter = 0;
            using (StreamReader r = new StreamReader(HostingEnvironment.MapPath("~/Data/flights.json")))
            {
                string json = r.ReadToEnd();
                dynamic array = JsonConvert.DeserializeObject(json);
                foreach (var flight in array)
                {
                    if (flight.sourceAirportId != null &&
                        flight.destinationAirportId != null &&
                        flight.airlineId != null)
                    {
                        var airline = defaultAirlines.Where(a => a.AirlineId == Convert.ToInt32(flight.airlineId) || Convert.ToString(flight.airlineCode).Contains(a.AirlineCodeIATA) || Convert.ToString(flight.airlineCode).Contains(a.AirlineCodeICAO)).FirstOrDefault();
                        var aircraft = defaultAircrafts.Where(a => Convert.ToString(flight.aircraftIata).Contains(a.AircraftCodeIATA) || Convert.ToString(flight.aircraftIata).Contains(a.AircraftCodeICAO)).FirstOrDefault();
                        var sourceAirport = defaultAirports.Where(a => a.AirportId == Convert.ToInt32(flight.sourceAirportId)).FirstOrDefault();
                        var destAirport = defaultAirports.Where(a => a.AirportId == Convert.ToInt32(flight.destinationAirportId)).FirstOrDefault();

                        if (airline != null && aircraft != null && sourceAirport != null && destAirport != null)
                        {
                            int flightTime = random.Next(5, 12);

                            for (int i = 0; i < 10; i++)
                            {
                                int[] minutes = { 0, 15, 30, 45 };
                                int month = random.Next(DateTime.Now.Month, 12);
                                int depDay = random.Next(1, 29);
                                int tempPrice = random.Next(flightTime * 800 / 5 - 100, flightTime * 800 / 5 + 100);
                                int price = tempPrice - tempPrice % 10;
                                int arrDay;
                                int depHour = random.Next(0, 23);
                                int arrHour = depHour + flightTime;
                                if (arrHour > 23)
                                {
                                    arrHour = arrHour - 23;
                                    arrDay = depDay + 1;
                                }
                                else
                                    arrDay = depDay;

                                int depMinute = random.Next(1, 3);
                                int arrMinute = random.Next(1, 3);

                                defaultFlights.Add(
                                    new Flight(
                                        ++flightCounter,
                                        $"{airline.AirlineCodeIATA}-{sourceAirport.AirportCodeIATA}-{destAirport.AirportCodeIATA}",
                                        airline.AirlineCodeIATA, airline.AirlineId, new DateTime(DateTime.Now.Year, month, depDay, depHour, minutes[depMinute], 00),
                                        sourceAirport.AirportId, sourceAirport.AirportCodeIATA,
                                        aircraft.AircraftCodeIATA, aircraft.AircraftId, aircraft.TotalPlaces,
                                        new DateTime(DateTime.Now.Year, month, arrDay, arrHour, minutes[arrMinute], 00),
                                        destAirport.AirportId, destAirport.AirportCodeIATA, Convert.ToInt32(flight.stops), price));
                            }
                        }
                    }
                }
            }

            await _flightService.BulkInsertFlights(defaultFlights);

            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }


        [ChildActionOnly]
        public ActionResult Flights(PaginationRequest request)
        {
            var flightList = new PagedList<FlightDto>
            {
                PageNumber = request.PageNumber,
                PageSize = 10
            };
            var m = new PaginationModel
            {
                FlightList = flightList,
                IsAdmin = true
            };
            var model = _flightService.GetFlightsOffset(m);
            return PartialView("AdminFlights", model);
        }

        [ChildActionOnly]
        public ActionResult Passengers(PaginationRequest request)
        {
            var passengerList = new PagedList<UserDto>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                UserList = passengerList,
                IsAdmin = true
            };
            var model = PassengerService.GetPassengersOffset(m);
            return PartialView("AdminPassengers", model);
        }

        [ChildActionOnly]
        public ActionResult Bookings(PaginationRequest request)
        {
            var bookingList = new PagedList<BookingDto>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                BookingList = bookingList,
                IsAdmin = true
            };
            var model = _bookingService.GetBookingsOffset(m);
            return PartialView("AdminBookings", model);
        }

        public PartialViewResult GetFlightsOffset(PaginationRequest request)
        {
            var flightList = new PagedList<FlightDto>
            {
                PageNumber = request.PageNumber,
                PageSize = 10
            };
            var m = new PaginationModel
            {
                FlightList = flightList,
                IsAdmin = true
            };
            var model = _flightService.GetFlightsOffset(m);
            return PartialView("AdminFlights", model);
        }

        public PartialViewResult GetPassengersOffset(PaginationRequest request)
        {
            var passengerList = new PagedList<UserDto>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                UserList = passengerList,
                IsAdmin = true
            };
            var model = PassengerService.GetPassengersOffset(m);
            return PartialView("AdminPassengers", model);
        }

        public PartialViewResult GetBookingsOffset(PaginationRequest request)
        {
            var bookingList = new PagedList<BookingDto>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                BookingList = bookingList,
                IsAdmin = true
            };
            var model = _bookingService.GetBookingsOffset(m);
            return PartialView("AdminBookings", model);
        }

        [HttpGet]
        public async Task<ActionResult> DeleteBooking(int bookingId)
        {
            _bookingService.IsExists(bookingId);
            _bookingService.IsThereAccess(CurrentUserId(), null);

            EmailService emailService = new EmailService();
            await emailService.SendNotificationEmailAsync(_bookingService.GetOwner(bookingId), null, null, bookingId);
            _notificationService.NewNotification(_bookingService.GetOwner(bookingId), bookingId, 0, 0);

            _bookingService.CancelBooking(bookingId);

            CommitService.Commit();
            return Json("Ok");
        }

        public ActionResult DeleteFlight(int flightId)
        {
            _flightService.IsExists(flightId);
            _flightService.IsThereAccess(CurrentUserId());
            _flightService.CancelFlight(flightId);
            CommitService.Commit();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteUser(int passengerId)
        {
            PassengerService.IsExists(passengerId);
            PassengerService.IsThereAccess(CurrentUserId());
            EmailService emailService = new EmailService();
            await emailService.SendNotificationEmailAsync(PassengerService.GetById(passengerId).Email, passengerId, null, null);
            PassengerService.Delete(passengerId);
            CommitService.Commit();

            return RedirectToAction("Index");
        }


        [ChildActionOnly]
        public ActionResult NewFlight()
        {
            var model = new NewFlightViewModel();
            return PartialView("AdminNewFlight", model);
        }

        [HttpPost]
        public ActionResult CreateNewFlight(NewFlightViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Json("Incorrect data");
            }

            var flight = _flightService.NewFlight(model);
            
            if(flight != null)
                return Json("Created");
            else
                return Json("Error");
        }

        [HttpGet]
        public ActionResult ChangeFlightStatus(int flightId)
        {
            _flightService.IsExists(flightId);
            var model = new FlightDelayModel()
            {
                FlightId = flightId,
                DelayTime = 1,
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult ChangeFlightStatus(FlightDelayModel model)
        {
            if(model.DelayTime > 3)
                return View(model);
            _flightService.GhangeFlightStatus(model.FlightId, model.DelayTime);
            var bookingList = _bookingService.GetAllByFlight(model.FlightId);

            foreach (var b in bookingList)
            {
                _notificationService.NewNotification(_bookingService.GetOwner(b.BookingId), 0, model.FlightId, model.DelayTime);
            }

            return RedirectToAction("Index");
        }

        public void ErrorTest()
        {
            throw new ApplicationException("This is the test exception!");
        }
    }
}