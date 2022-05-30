using SAMAirline.Logic.Interfaces;
using System.Web.Mvc;

namespace SAMAirline.Website.Controllers
{
    public class BaseController : Controller
    {
        public IPassengerService PassengerService { get; set; }
        public INotificationService NotificationService { get; set; }
        public ICommitService CommitService { get; set; }

        public int CurrentUserId()
        {
            return PassengerService.GetByEmail(User.Identity.Name).UserId;
        }
    }
}