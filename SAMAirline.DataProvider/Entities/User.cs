    namespace SAMAirline.DataProvider.Entities
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("User")]
    public partial class User
    {
        [Key]
        public int UserId { get; set; }

        public byte[] ProfileImage { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Surname { get; set; }

        [Required]
        [StringLength(50)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; }

        [Required]
        public bool IsConfirmed { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }
    }
}
