using SAMAirline.Common;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SAMAirline.DataProvider.Entities
{
    public partial class Airline
    {
        [Key]
        public int AirlineId { get; set; }

        [Required]
        [StringLength(2)]
        public string AirlineCodeIATA { get; set; }
        [Required]
        [StringLength(3)]
        public string AirlineCodeICAO { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }


        public virtual ICollection<Flight> Flights { get; set; }

        public Airline()
        {
        }

        public Airline(int airlineId, string airlineCodeIATA, string airlineCodeICAO, string name, string country)
        {
            AirlineId = airlineId;
            AirlineCodeIATA = airlineCodeIATA;
            AirlineCodeICAO = airlineCodeICAO;
            Name = name;
            Country = country;
        }
    }
}
