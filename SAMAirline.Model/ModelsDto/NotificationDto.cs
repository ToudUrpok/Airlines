using System;
namespace SAMAirline.Model.ModelsDto
{
    public class NotificationDto
    {
        public int NotificationId { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
    }
}
