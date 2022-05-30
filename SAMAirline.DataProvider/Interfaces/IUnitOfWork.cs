namespace SAMAirline.DataProvider.Interfaces
{
    public interface IUnitOfWork
    {
        IAircraftRepository Aircrafts { get; }
        IAirportRepository Airports { get; }
        IBookingRepository Bookings { get; }
        IFlightRepository Flights { get; }
        INotificationRepository Notifications { get; }
        IPassengerRepository Passengers { get; }
        void Save();
    }
}
