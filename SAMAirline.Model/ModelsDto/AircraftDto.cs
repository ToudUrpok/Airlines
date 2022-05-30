namespace SAMAirline.Model.ModelsDto
{
    public class AircraftDto
    {
        public int AircraftId { get; set; }
        public string AircraftCodeIATA { get; set; }
        public string AircraftCodeICAO { get; set; }
        public string Model { get; set; }
        public int TotalPlaces { get; set; }
    }
}
