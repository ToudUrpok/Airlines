namespace SAMAirline.DataProvider.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Booking")]
    public partial class Booking
    {
        [Key]
        public int BookingId { get; set; }

        [Required]
        public int FlightId { get; set; }
        public virtual Flight Flight { get; set; }

        [Required]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [Required]
        public string PassengerName { get; set; }
        [Required]
        public string PassengerSurname { get; set; }
        [Required]
        public string PassengerNationality { get; set; }
        [Required]
        public string PassengerBirthdate { get; set; }
        [Required]
        public string PassengerPassport { get; set; }
        [Required]
        public string PassportExpire { get; set; }

        [Required]
        public DateTime BookingDateTime { get; set; }

        [Required]
        public string ContactPhoneNumber { get; set; }
        [Required]
        public string ContactEmail { get; set; }

        [Required]
        public int Price { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
