using SAMAirline.Model.ModelsDto;
using System.Collections.Generic;

namespace SAMAirline.Model.Models
{
    public class NotificationWindowViewModel
    {
        public IEnumerable<NotificationDto> List { get; set; }
        public int Count { get; set; }
    }
}
