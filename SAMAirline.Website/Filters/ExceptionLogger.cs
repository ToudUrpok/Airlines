using SAMAirline.DataProvider;
using SAMAirline.DataProvider.Entities;
using System;
using System.Web.Mvc;

namespace SAMAirline.Website.Filters
{
    public class ExceptionLogger : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext exceptionContext)
        {
            LoggingError exceptionDetail = new LoggingError()
            {
                ExceptionMessage = exceptionContext.Exception.Message,
                StackTrace = exceptionContext.Exception.StackTrace,
                ControllerName = exceptionContext.RouteData.Values["controller"].ToString(),
                ActionName = exceptionContext.RouteData.Values["action"].ToString(),
                Date = DateTime.Now
            };
            using (AirlineDB db = new AirlineDB())
            {
                db.LoggingErrors.Add(exceptionDetail);
                try
                { db.SaveChanges(); }
                catch (Exception e) { }
            }
            exceptionContext.ExceptionHandled = true;
            exceptionContext.Result = new RedirectResult("/Home/AppError");
        }
    }
}