using SAMAirline.Model.ModelsDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class NewBookingViewModel
    {
        public int OwnerId { get; set; }

        public int FlightId { get; set; }
        public FlightDto Flight { get; set; }

        public int SecondFlightId { get; set; }
        public FlightDto SecondFlight { get; set; }

        public List<PassengerInfo> PassengerInfos { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "PhoneNumberRequired")]
        [Display(Name = "PhoneNumber", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[0-9]{7,12}", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectPhoneNumber")]
        public string ContactPhoneNumber { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectEmail")]
        public string ContactEmail { get; set; }

        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "EmailRequired")]
        [Display(Name = "Email", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectEmail")]
        public string ConfirmEmail { get; set; }

        public bool IsActive { get; set; }
        public string Message { get; set; }

        public int TotalPrice { get; set; }

        [Required]
        [RegularExpression(@"[1-9]", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectAmount")]
        public int Amount { get; set; }
    }
}
