using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Model.PaginationModels;
using SAMAirline.Website.Filters;
using System.Web.Mvc;

namespace SAMAirline.Website.Controllers
{
    [Culture]
    [ExceptionLogger]

    public class FlightController : BaseController
    {
        private readonly IBookingService _bookingService;
        private readonly IFlightService _flightService;
        public const int Amount = 5;

        public FlightController(
            IFlightService flightService,
            IBookingService bookingService)
        {
            _bookingService = bookingService;
            _flightService = flightService;
        }


        [HttpGet]
        public ActionResult Index(bool? backToList)
        {
            var model = new FlightPaginationModel();

            if (backToList != null || Session["LastSearch"] != null)
                model = (FlightPaginationModel)Session["LastSearch"];
            else
            {
                var flightList = new PagedList<FlightDto>();
                flightList.PageNumber = 1;
                flightList.PageSize = Amount;
                model.FlightList = flightList;
                model = _flightService.GetNeededFlightsOffset(model);
                Session["LastSearch"] = model;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(bool? backToList, FlightPaginationModel model)
        {
            if (model == null)
                model = new FlightPaginationModel();

            if (backToList != null && Session["LastSearch"] != null)
                model = (FlightPaginationModel)Session["LastSearch"];
            else
            {
                var flightList = new PagedList<FlightDto>();
                flightList.PageNumber = model.PageNumber;
                flightList.PageSize = Amount;
                model.FlightList = flightList;

                var twoWayFlightsList = new PagedList<TwoWayFlightDto>();
                twoWayFlightsList.PageNumber = model.PageNumber;
                twoWayFlightsList.PageSize = Amount;
                model.TwoWayFlightList = twoWayFlightsList;

                if (model.ReturnDate != null)
                    model.IsTwoWay = true;

                model = _flightService.GetNeededFlightsOffset(model);
                if (model.FlightList.RowsCount == 0 || (model.TwoWayFlightList.RowsCount == 0 && model.IsTwoWay))
                {
                    model.GetNearby = true;
                    model = _flightService.GetNeededFlightsOffset(model);
                    model.FlightList.Message = Resources.Resource.NoFlightsTheseDay;
                }
                if (model.FlightList.RowsCount == 0)
                {
                    model.FlightList.Message = Resources.Resource.NoFlightsFound;
                }
                Session["LastSearch"] = model;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult GetFlights(FlightPaginationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var flightList = new PagedList<FlightDto>();
            flightList.PageNumber = model.PageNumber;
            flightList.PageSize = Amount;
            model.FlightList = flightList;

            var twoWayFlightsList = new PagedList<TwoWayFlightDto>();
            twoWayFlightsList.PageNumber = model.PageNumber;
            twoWayFlightsList.PageSize = Amount;
            model.TwoWayFlightList = twoWayFlightsList;

            if (model.ReturnDate != null)
                model.IsTwoWay = true;

            model = _flightService.GetNeededFlightsOffset(model);
            if (model.FlightList.RowsCount == 0)
            {
                model.GetNearby = true;
                model = _flightService.GetNeededFlightsOffset(model);
                model.FlightList.Message = Resources.Resource.NoFlightsTheseDay;
            }
            if (model.FlightList.RowsCount == 0)
            {
                model.FlightList.Message = Resources.Resource.NoFlightsFound;
                model.NoResult = true;
            }
            Session["LastSearch"] = model;
            return PartialView("FlightPartialView", model);
        }


        [HttpPost]
        public ActionResult GetFlightsOffset(FlightPaginationModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var flightList = new PagedList<FlightDto>();
            flightList.PageNumber = model.PageNumber;
            flightList.PageSize = Amount;
            model.FlightList = flightList;

            var twoWayFlightsList = new PagedList<TwoWayFlightDto>();
            twoWayFlightsList.PageNumber = model.PageNumber;
            twoWayFlightsList.PageSize = Amount;
            model.TwoWayFlightList = twoWayFlightsList;

            if (model.ReturnDate != null)
                model.IsTwoWay = true;

            model = _flightService.GetNeededFlightsOffset(model);
            if (model.FlightList.RowsCount == 0)
            {
                model.GetNearby = true;
                model = _flightService.GetNeededFlightsOffset(model);
                model.FlightList.Message = Resources.Resource.NoFlightsTheseDay;
            }
            if (model.FlightList.RowsCount == 0)
            {
                model.FlightList.Message = Resources.Resource.NoFlightsFound;
            }
            Session["LastSearch"] = model;
            return PartialView("FlightPartialView", model);
        }
    }
}
