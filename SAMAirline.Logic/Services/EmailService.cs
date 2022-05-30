using MimeKit;
using MailKit.Net.Smtp;
using System.Threading.Tasks;
using System.Configuration;
using SAMAirline.Model.ModelsDto;
using SAMAirline.Logic.Interfaces;

namespace SAMAirline.Logic.Services
{
    public class EmailService : IEmailService
    {
        public async Task SendNotificationEmailAsync(string email, int? userId, int? ticketId, int? bookingId)
        {
            var emailMessage = new MimeMessage();
            var body = "";
            if (userId.HasValue)
                body += $"We apologize but your account and all your tickets and bookings has been deleted by the moderator.";
            else if (ticketId.HasValue)
                body += $"We apologize but your ticket {ticketId} has been deleted by the moderator.";
            else if (bookingId.HasValue)
                body += $"We apologize but your booking {bookingId} has been deleted by the moderator.";

            emailMessage.From.Add(new MailboxAddress("Booking notification", "yakubovih_e@inbox.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Notification";
            emailMessage.Body = new TextPart("Plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("mailbe05.hoster.by", 587, false);
                await client.AuthenticateAsync(ConfigurationManager.AppSettings["AdminEmailAdress"].ToString(), ConfigurationManager.AppSettings["AdminEmailPassword"].ToString());
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async Task SendBookingNotificationAsync(string email, BookingDto booking)
        {
            var emailMessage = new MimeMessage();
            var body = "";

            body += $"Dear, {email}, your booking {booking.BookingId} from {booking.DepAirportCode} to {booking.ArrAirportCode} will end soon, you must pay for your tickets or your payment will be cleared";

            emailMessage.From.Add(new MailboxAddress("Booking notification", "yakubovih_e@inbox.ru"));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Notification";
            emailMessage.Body = new TextPart("Plain")
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("mailbe05.hoster.by", 587, false);
                await client.AuthenticateAsync(ConfigurationManager.AppSettings["AdminEmailAdress"].ToString(), ConfigurationManager.AppSettings["AdminEmailPassword"].ToString());
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }

        public async Task SendMessageAsync(string email, string body)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress("Booking notification", ConfigurationManager.AppSettings["AdminEmailAdress"].ToString()));
            emailMessage.To.Add(new MailboxAddress("", email));
            emailMessage.Subject = "Notification";
            emailMessage.Body = new TextPart("Plain")
            {
                Text = body
            };
            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("mailbe05.hoster.by", 587, false);
                await client.AuthenticateAsync(ConfigurationManager.AppSettings["AdminEmailAdress"].ToString(), ConfigurationManager.AppSettings["AdminEmailPassword"].ToString());
                await client.SendAsync(emailMessage);

                await client.DisconnectAsync(true);
            }
        }
    }
}
