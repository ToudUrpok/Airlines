using SAMAirline.Logic.Interfaces;
using SAMAirline.Logic.Services;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Website.Filters;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace SAMAirline.Website.Controllers
{
    [Culture]
    [ExceptionLogger]
    public class AccountController : BaseController
    {
        public AccountController() { }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var redirectUrl = Session["RedirectUrl"];
            var logmodel = PassengerService.Login(model);
            if (logmodel.Message != null)
            {
                logmodel.Email = model.Email;
                logmodel.Message = Resources.Resource.LogError;
                return View(logmodel);
            }
            if (!logmodel.IsConfirmed)
                return RedirectToAction("Confirm", "Account", new { model.Email });
            FormsAuthentication.SetAuthCookie(model.Email, true);
            if (redirectUrl != null)
            {
                Session["RedirectUrl"] = null;
                return Redirect(Convert.ToString(redirectUrl));
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Registration()
        {
            return View(new RegistrationViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Registration(RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var redirectUrl = Session["RedirectUrl"];
            var regmodel = PassengerService.Registration(model);
            if (regmodel.Message != null)
            {
                regmodel.Message = Resources.Resource.RegError;
                return View(regmodel);
            }
            var body = string.Format("{0}: {1}", Resources.Resource.ConfirmationLink,
                Url.Action("ConfirmEmail", "Account", new { Token = regmodel.Id, regmodel.Email }, Request.Url.Scheme));
            EmailService emailService = new EmailService();
            await emailService.SendMessageAsync(regmodel.Email, body);
            return RedirectToAction("Confirm", "Account", new { model.Email });
        }

        [HttpGet]
        public ActionResult ForgotPassword()
        {
            return View(new EditUserInfoViewModel());
        }

        [HttpPost]
        public async Task<ActionResult> ForgotPassword(EditUserInfoViewModel model)
        {
            if (model.Email.Length == 0 || !PassengerService.IsExists(model.Email))
                return Json("Error");
            else
            {
                var random = new Random();
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var newPassword = new string(Enumerable.Repeat(chars, 8).Select(s => s[random.Next(s.Length)]).ToArray());

                model.Id = PassengerService.GetByEmail(model.Email).UserId;
                model.NewPassword = newPassword;
                PassengerService.ChangePassword(model);

                EmailService emailService = new EmailService();
                await emailService.SendMessageAsync(model.Email, $"You can login into your account using this password: {newPassword}");

                return Json("Ok");
            };
        }

        [AllowAnonymous]
        public ActionResult Confirm(string Email)
        {
            ViewData["Message"] = string.Format("{0} {1}", Resources.Resource.ConfirmationMessage, Email);
            return View();
        }

        [AllowAnonymous]
        public ActionResult ConfirmEmail(int Token, string Email)
        {
            var user = PassengerService.GetById(Token);
            if (user == null)
                return RedirectToAction("Confirm", "Account", new { Email = "" });
            if (user.Email != Email)
                return RedirectToAction("Confirm", "Account", new { user.Email });
            PassengerService.ConfirmEmail(Email);
            return RedirectToAction("Login", "Account");
        }

        [Authorize]
        public ActionResult MyAccount()
        {
            var passenger = PassengerService.GetById(CurrentUserId());
            return View(passenger);
        }

        [Authorize]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }


        [HttpPost]
        [Authorize]
        public ActionResult EditInfo(EditUserInfoViewModel model)
        {
            model.Id = CurrentUserId();
            PassengerService.EditInfo(model);
            CommitService.Commit();
            return RedirectToAction("MyAccount");
        }

        [HttpPost]
        [Authorize]
        public ActionResult ChangePassword(EditUserInfoViewModel model)
        {
            model.Id = CurrentUserId();
            PassengerService.ChangePassword(model);
            CommitService.Commit();
            return RedirectToAction("MyAccount");
        }

        [HttpPost]
        public ActionResult Upload(UserDto model, HttpPostedFileBase photo)
        {
            if (photo == null)
                ModelState.AddModelError(string.Empty, "Please choose file");
            else
            {
                byte[] imageData = null;
                using (var binaryReader = new BinaryReader(photo.InputStream))
                {
                    imageData = binaryReader.ReadBytes(photo.ContentLength);
                }

                model.ProfileImage = imageData;
                PassengerService.UploadProfileImage(CurrentUserId(), imageData);
            }
            return RedirectToAction("MyAccount");
        }

        [HttpGet]
        [Authorize]
        public ActionResult DeleteNotification(int notificationId)
        {
            NotificationService.DeleteNotification(notificationId);
            return new HttpStatusCodeResult(200);
        }
    }
}
