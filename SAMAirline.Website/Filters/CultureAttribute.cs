using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace SAMAirline.Website.Filters
{
    public class CultureAttribute : FilterAttribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            string cultureName = null;
            HttpCookie cultureCookie = filterContext.HttpContext.Request.Cookies["lang"];
            if (cultureCookie != null)
                cultureName = cultureCookie.Value;
            else
                cultureName = "en";

            // Список культур
            List<string> cultures = new List<string>() { "ru", "en" };
            if (!cultures.Contains(cultureName))
            {
                cultureName = "en";
            }
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(cultureName);
            Thread.CurrentThread.CurrentUICulture = CultureInfo.CreateSpecificCulture(cultureName);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //не реализован
        }
    }
}