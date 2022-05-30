using SAM.Airline.Logic.Interfaces;
using SAM.Airline.Logic.Services;
using SAM.Airline.Model.Models;
using SAM.Airline.Website.Filters;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SAM.Airline.Website.Controllers
{
    [Culture]
    [ExceptionLogger]
    [Authorize(Roles = "Admin")]
    public class AdminController : BaseController
    {
        private readonly IFlightService _flightService;
        private readonly ITicketService _ticketService;
        private readonly IBookingService _bookingService;
        private readonly IAircraftService _aircraftService;
        private readonly IAirportService _airportService;
        private readonly INotificationService _notificationService;

        public const int Amount = 3;

        public AdminController(
            IFlightService flightService,
            IBookingService bookingService,
            ITicketService ticketService,
            IAircraftService aircraftService,
            IAirportService airportService,
            INotificationService notificationService)
        {
            _ticketService = ticketService;
            _bookingService = bookingService;
            _flightService = flightService;
            _aircraftService = aircraftService;
            _airportService = airportService;
            _notificationService = notificationService;
        }

        EmailService emailService = new EmailService();

        public ActionResult Index()
        {
            var model = new PaginationModel();
            model.AircraftList = new SelectList(_aircraftService.GetAll(), "AircraftCode", "AircraftCode");
            model.AirportList = new SelectList(_airportService.GetAll(), "AirportCode", "Name");
            return View(model);
        }

        [ChildActionOnly]
        public ActionResult Flights(PaginationRequest request)
        {
            var flightList = new PagedList<FlightViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = 5
            };
            var m = new PaginationModel
            {
                FlightList = flightList
            };
            var model = _flightService.GetFlightsOffset(m);
            return PartialView("AdminFlights", model);
        }

        [ChildActionOnly]
        public ActionResult Passengers(PaginationRequest request)
        {
            var passengerList = new PagedList<PassengerViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                PassengerList = passengerList
            };
            var model = PassengerService.GetPassengersOffset(m);
            return PartialView("AdminPassengers", model);
        }

        [ChildActionOnly]
        public ActionResult Bookings(PaginationRequest request)
        {
            var bookingList = new PagedList<BookingViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                BookingList = bookingList
            };
            var model = _bookingService.GetBookingsOffset(m);
            return PartialView("AdminBookings", model);
        }

        [ChildActionOnly]
        public ActionResult Tickets(PaginationRequest request)
        {
            var ticketList = new PagedList<TicketViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                TicketList = ticketList
            };
            var model = _ticketService.GetTicketsOffset(m);
            return PartialView("AdminTickets", model);
        }

        public PartialViewResult GetFlightsOffset(PaginationRequest request)
        {
            var flightList = new PagedList<FlightViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = 5
            };
            var m = new PaginationModel
            {
                FlightList = flightList
            };
            var model = _flightService.GetFlightsOffset(m);
            return PartialView("AdminFlights", model);
        }

        public PartialViewResult GetPassengersOffset(PaginationRequest request)
        {
            var passengerList = new PagedList<PassengerViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                PassengerList = passengerList
            };
            var model = PassengerService.GetPassengersOffset(m);
            return PartialView("AdminPassengers", model);
        }

        public PartialViewResult GetBookingsOffset(PaginationRequest request)
        {
            var bookingList = new PagedList<BookingViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                BookingList = bookingList
            };
            var model = _bookingService.GetBookingsOffset(m);
            return PartialView("AdminBookings", model);
        }

        public PartialViewResult GetTicketsOffset(PaginationRequest request)
        {
            var ticketList = new PagedList<TicketViewModel>
            {
                PageNumber = request.PageNumber,
                PageSize = Amount
            };
            var m = new PaginationModel
            {
                TicketList = ticketList
            };
            var model = _ticketService.GetTicketsOffset(m);
            return PartialView("AdminTickets", model);
        }

        public async Task<ActionResult> DeleteBooking(int bookingId)
        {
            _bookingService.IsExists(bookingId);
            _bookingService.IsThereAccess(CurrentUserId(), null);
            EmailService emailService = new EmailService();
            await emailService.SendNotificationEmailAsync(_bookingService.GetOwner(bookingId), null, null, bookingId);
            _notificationService.NewNotification(_bookingService.GetOwner(bookingId), 0, bookingId, 0, 0);
            _bookingService.CancelBooking(bookingId);
            CommitService.Commit();
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> DeleteTicket(int ticketId)
        {
            _ticketService.IsExists(ticketId);
            _ticketService.IsThereAccess(CurrentUserId(), null);
            _ticketService.DeleteTicket(ticketId, null);
            EmailService emailService = new EmailService();
            await emailService.SendNotificationEmailAsync(_ticketService.GetOwner(ticketId), null, ticketId, null);
            _notificationService.NewNotification(_ticketService.GetOwner(ticketId), ticketId, 0, 0, 0);
            CommitService.Commit();
            return RedirectToAction("Index");
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
            model.AircraftList = new SelectList(_aircraftService.GetAll(), "AircraftCode", "AircraftCode");
            model.AirportList = new SelectList(_airportService.GetAll(), "AirportCode", "Name");
            return PartialView("AdminNewFlight", model);
        }

        [HttpPost]
        public ActionResult CreateNewFlight(NewFlightViewModel model)
        {
            _flightService.NewFlight(model);
            CommitService.Commit();
            return RedirectToAction("Index");
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
            _flightService.GhangeFlightStatus(model.FlightId, model.DelayTime);
            var ticketList = _ticketService.GetAllByFlight(model.FlightId);
            var bookingList = _bookingService.GetAllByFlight(model.FlightId);
            foreach (var t in ticketList)
            {
                _notificationService.NewNotification(_ticketService.GetOwner(t.TicketId), 0, 0, model.FlightId, model.DelayTime);
            }
            foreach (var b in bookingList)
            {
                _notificationService.NewNotification(_bookingService.GetOwner(b.BookingId), 0, 0, model.FlightId, model.DelayTime);
            }
            return RedirectToAction("Index");
        }

        public void ErrorTest()
        {
            throw new ApplicationException("This is the test exception!");
        }
    }
}