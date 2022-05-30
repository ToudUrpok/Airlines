using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class NewTicketViewModel
    {
        public int FlightId { get; set; }

        public int Amount { get; set; }

        public List<PassengerInfo> Passengers { get; set; }

        public int TotalPrice { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "CardRequired")]
        [Display(Name = "Card", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[0-9]{16}", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectCardNumber")]
        public string CardNumber { get; set; }

        public NewTicketViewModel()
        {
            Passengers = new List<PassengerInfo>();
        }

    }
}
