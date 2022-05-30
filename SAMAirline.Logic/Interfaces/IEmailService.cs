using SAMAirline.Model.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Interfaces
{
    public interface IEmailService
    {
        Task SendNotificationEmailAsync(string email, int? userId, int? ticketId, int? bookingId);
        Task SendBookingNotificationAsync(string email, BookingDto booking);
        Task SendMessageAsync(string email, string body);
    }
}
