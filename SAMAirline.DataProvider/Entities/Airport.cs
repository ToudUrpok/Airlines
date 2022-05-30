namespace SAMAirline.DataProvider.Entities
{
    using SAMAirline.Common;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Airport")]
    public partial class Airport
    {
        [Key]
        public int AirportId { get; set; }

        [Required]
        [StringLength(3)]
        public string AirportCodeIATA { get; set; }

        [Required]
        [StringLength(4)]
        public string AirportCodeICAO { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Country { get; set; }

        [Required]
        [StringLength(50)]
        public string City { get; set; }

        public virtual ICollection<Flight> DepFlights { get; set; }

        public virtual ICollection<Flight> ArrFlights { get; set; }

        public Airport()
        {
        }

        public Airport(int airportId, string airportCodeIATA, string airportCodeICAO, string name, string country, string city)
        {
            AirportId = airportId;
            AirportCodeIATA = airportCodeIATA;
            AirportCodeICAO = airportCodeICAO;
            Name = name;
            Country = country;
            City = city;
        }
    }
}
