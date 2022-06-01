using SAMAirline.DataProvider.Entities;
using System.Data.Entity;

namespace SAMAirline.DataProvider
{
    public partial class AirlineDB : DbContext
    {
        public AirlineDB()
            : base(@"Data Source=localhost\SQLEXPRESS;Initial Catalog=Airline1;Integrated Security=True;MultipleActiveResultSets=True")
        {
            Database.SetInitializer(new AirlineDBInitializer());
        }

        public virtual DbSet<Airline> Airlines { get; set; }
        public virtual DbSet<Aircraft> Aircrafts { get; set; }
        public virtual DbSet<Airport> Airports { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Notification> Notifications { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<LoggingError> LoggingErrors { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Airline>()
                .HasMany(a => a.Flights)
                .WithRequired(e => e.Airline)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Aircraft>()
                .HasMany(a => a.Flights)
                .WithRequired(e => e.Aircraft)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Airport>()
                .HasMany(e => e.DepFlights)
                .WithRequired(e => e.DepAirport)
                .HasForeignKey(e => e.DepartureAirportId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Airport>()
                .HasMany(e => e.ArrFlights)
                .WithRequired(e => e.ArrAirport)
                .HasForeignKey(e => e.ArrivalAirportId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.Flight)
                .WithMany(b => b.Bookings)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Booking>()
                .HasRequired(b => b.User)
                .WithMany(b => b.Bookings)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Bookings)
                .WithRequired(e => e.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(e => e.Notifications)
                .WithRequired(e => e.User)
                .HasForeignKey(e => e.UserId)
                .WillCascadeOnDelete(false);
        }
    }
}
