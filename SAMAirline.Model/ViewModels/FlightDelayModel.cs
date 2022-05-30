using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Model.Models
{
    public class FlightDelayModel
    {
        public int FlightId { get; set; }
        [Required]
        [RegularExpression(@"[1-3]", ErrorMessageResourceType = typeof(Resources.Resource),
                  ErrorMessageResourceName = "IncorrectDelay")]
        public int DelayTime { get; set; }
    }
}
