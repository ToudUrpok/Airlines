using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class SearchFlightViewModel
    {
        [Display(Name = "From", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[A-Za-z]+", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectDepartureCity")]
        public string From { get; set; }

        [Display(Name = "To", ResourceType = typeof(Resources.Resource))]
        [RegularExpression(@"[A-Za-z]+", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectArrivalCity")]
        public string To { get; set; }

        public DateTime Date { get; set; }
    }
}
