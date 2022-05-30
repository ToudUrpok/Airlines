namespace SAMAirline.DataProvider.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Aircraft
    {
        [Key]
        public int AircraftId { get; set; }

        [Required]
        [StringLength(3)]
        public string AircraftCodeIATA { get; set; }
        [Required]
        [StringLength(4)]
        public string AircraftCodeICAO { get; set; }

        [Required]
        [StringLength(100)]
        public string Model { get; set; }

        [Required]
        public int TotalPlaces { get; set; }

        public virtual ICollection<Flight> Flights { get; set; }

        public Aircraft()
        {
        }

        public Aircraft(int aircraftId, string aircraftCodeIATA, string aircraftCodeICAO, string name, int totalPlaces)
        {
            AircraftId = aircraftId;
            AircraftCodeIATA = aircraftCodeIATA;
            AircraftCodeICAO = aircraftCodeICAO;
            Model = name;
            TotalPlaces = totalPlaces;
        }
    }
}
