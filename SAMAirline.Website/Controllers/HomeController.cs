using SAMAirline.Logic.Interfaces;
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
    public class HomeController : BaseController
    {
        private readonly IFlightService _flightService;

        public HomeController(
            IFlightService flightService)
        {
            _flightService = flightService;
        }

        public const int Amount = 10;

        //[HttpGet]
        //public ActionResult Index(bool? backToList)
        //{
        //    FlightPaginationModel model = new FlightPaginationModel();
        //    if (backToList != null && Session["LastSearch"] != null)
        //        model = (FlightPaginationModel)Session["LastSearch"];
        //    else
        //    {
        //        var flightList = new PagedList<FlightViewModel>();
        //        flightList.PageNumber = 0;
        //        flightList.PageSize = Amount;
        //        model.FlightList = flightList;
        //        model.OrderAscending = true;
        //        model.SortByDate = true;
        //        model = _flightService.GetNeededFlightsOffset(model);
        //        Session["LastSearch"] = model;
        //    }
        //    return View(model);
        //}


        [HttpGet]
        public ActionResult Index()
        {
            //var flights = _flightService.GetAll();

            return View();
        }

        public ActionResult ChangeCulture(string lang)
        {
            string returnUrl = Request.UrlReferrer.AbsoluteUri;
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(lang))
            {
                lang = "en";
            }
            HttpCookie cookie = Request.Cookies["lang"];
            if (cookie != null)
                cookie.Value = lang;
            else
            {
                cookie = new HttpCookie("lang");
                cookie.HttpOnly = false;
                cookie.Value = lang;
                cookie.Expires = DateTime.Now.AddYears(1);
            }
            Response.Cookies.Add(cookie);
            return Redirect(returnUrl);
        }

        //public ActionResult NewBooking(int flightId)
        //{
        //    _flightService.IsExists(flightId);
        //    if (User.Identity.IsAuthenticated)
        //        return RedirectToAction("NewBooking", "Booking", new { FlightId = flightId });
        //    else
        //    {
        //        Session["RedirectUrl"] = Url.Action("NewBooking", "Booking", new { FlightId = flightId });
        //        return RedirectToAction("Login", "Account");
        //    }
        //}

        public ActionResult AppError()
        {
            return View();
        }

        public ActionResult NotFound()
        {
            return View();
        }
    }
}