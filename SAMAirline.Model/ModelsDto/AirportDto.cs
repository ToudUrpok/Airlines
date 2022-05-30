namespace SAMAirline.Model.ModelsDto
{
    public class AirportDto
    {
        public int AirportId { get; set; }
        public string AirportCodeIATA { get; set; }
        public string AirportCodeICAO { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
