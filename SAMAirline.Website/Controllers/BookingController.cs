using SAMAirline.Logic.Interfaces;
using SAMAirline.Logic.Services;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Model.PaginationModels;
using SAMAirline.Website.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SAMAirline.Website.Controllers
{
    [Culture]
    [ExceptionLogger]
    public class BookingController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IFlightService _flightService;
        private readonly IPassengerService _passengerService;
        public const int Amount = 3;

        public BookingController(
            IFlightService flightService,
            IBookingService bookingService,
            IPassengerService passengerService)
        {
            _bookingService = bookingService;
            _flightService = flightService;
            _passengerService = passengerService;
        }

        [Authorize]
        public ActionResult Index()
        {
            var bookingList = new PagedList<BookingDto>();
            bookingList.PageNumber = 100;
            bookingList.PageSize = Amount;
            var model = new PaginationModel();
            model.UserId = CurrentUserId();
            model.BookingList = bookingList;
            model = _bookingService.GetBookingsOffset(model);
            return View(model);
        }

        public PartialViewResult GetBookingsOffset(PaginationRequest number)
        {
            var bookingList = new PagedList<BookingDto>();
            bookingList.PageNumber = number.PageNumber;
            bookingList.PageSize = Amount;
            var model = new PaginationModel();
            model.UserId = CurrentUserId();
            model.BookingList = bookingList;
            model = _bookingService.GetBookingsOffset(model);
            return PartialView("~/Views/Shared/BookingPartialView.cshtml", model);
        }

        [HttpGet]
        public ActionResult NewBooking(int flightId, int secondFlightId, int amount)
        {
            if (User.Identity.IsAuthenticated == false)
            {
                Session["RedirectUrl"] = Url.Action("NewBooking", "Booking", new { flightId, secondFlightId, amount });
                return RedirectToAction("Login", "Account");
            }

            _flightService.IsExists(flightId);
            var flight = _flightService.GetById(flightId);

            if (_bookingService.IsAlreadyBooked(CurrentUserId(), flightId, amount))
            {
                var alreadyBooked = _bookingService.GetUsersBooking(CurrentUserId()).ToList();

                ViewBag.Count = 9 - alreadyBooked.Count();

                return View("AlreadyBookedView");
            }

            var model = new NewBookingViewModel()
            {
                FlightId = flightId,
                Flight = flight,
                Amount = amount
            };

            if (secondFlightId != 0)
            {
                _flightService.IsExists(secondFlightId);
                var secondFlight = _flightService.GetById(secondFlightId);

                if (_bookingService.IsAlreadyBooked(CurrentUserId(), secondFlightId, amount))
                    return View("AlreadyBookedView");

                model.SecondFlightId = secondFlightId;
                model.SecondFlight = secondFlight;

                model.TotalPrice = (model.Flight.Price + model.SecondFlight.Price) * amount;
            }
            else
                model.TotalPrice = model.Flight.Price * amount;

            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> NewBooking(NewBookingViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Error");
            }
            int totalPrice = 0;
            int ticketsCount = 0;
            if (model.FlightId != 0)
            {
                model.Flight = _flightService.GetById(model.FlightId);
                totalPrice += model.Flight.Price;
                ticketsCount++;
            }
            if (model.SecondFlightId != 0)
            {
                model.SecondFlight = _flightService.GetById(model.SecondFlightId);
                totalPrice += model.SecondFlight.Price;
                ticketsCount++;
            }
            totalPrice *= model.PassengerInfos.Count();

            var user = _passengerService.GetById(CurrentUserId());

            EmailService emailService = new EmailService();

            ticketsCount *= model.PassengerInfos.Count();

            var body = $"Dear {user.Name} {user.Surname}, you just book {ticketsCount} ticket(s) for flight ";
            body += $"number {model.Flight.FlightNumber} from {model.Flight.DepartureCity} {model.Flight.DepartureCountry}  to {model.Flight.ArrivalCity}  {model.Flight.ArrivalCountry} ";
            body += model.SecondFlight != null ? $"and second flight number {model.SecondFlight.FlightNumber} from {model.SecondFlight.DepartureCity} {model.SecondFlight.DepartureCountry}  to {model.SecondFlight.ArrivalCity}  {model.SecondFlight.ArrivalCountry}" : "";
            body += $". Total price is ${(int)(totalPrice * 1.1)}. ";

            await emailService.SendMessageAsync(user.Email, body);

            _bookingService.NewBooking(CurrentUserId(), model);

            return RedirectToAction("Index", "Booking");
        }

        [HttpGet]
        public ActionResult ExtendBooking(int bookingId)
        {
            _bookingService.ChangeStatus(bookingId, true);

            return RedirectToAction("Index", "Booking");
        }


        public ActionResult CancelBooking(int bookingId)
        {
            _bookingService.IsThereAccess(CurrentUserId(), bookingId);
            _bookingService.CancelBooking(bookingId);
            CommitService.Commit();
            return RedirectToAction("Index", "Booking");
        }

        public ActionResult NewTicket(int bookingId)
        {
            _bookingService.IsThereAccess(CurrentUserId(), bookingId);
            return RedirectToAction("NewTicket", "Ticket", new { bookingId });
        }
    }
}
