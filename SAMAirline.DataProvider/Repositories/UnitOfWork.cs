using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.DataProvider.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AirlineDB _context;

        public UnitOfWork(AirlineDB context)
        {
            _context = context;
        }

        private AircraftRepository _aircraftRepository;
        private AirportRepository _airportRepository;
        private BookingRepository _bookingRepository;
        private FlightRepository _flightRepository;
        private NotificationRepository _notificationRepository;
        private PassengerRepository _passengerRepository;

        public IAircraftRepository Aircrafts
        {
            get
            {
                if (_aircraftRepository == null)
                {
                    _aircraftRepository = new AircraftRepository(_context);
                }
                return _aircraftRepository;
            }
        }
        public IAirportRepository Airports
        {
            get
            {
                if (_airportRepository == null)
                {
                    _airportRepository = new AirportRepository(_context);
                }
                return _airportRepository;
            }
        }

        public IBookingRepository Bookings
        {
            get
            {
                if (_bookingRepository == null)
                {
                    _bookingRepository = new BookingRepository(_context);
                }
                return _bookingRepository;
            }
        }

        public IFlightRepository Flights
        {
            get
            {
                if (_flightRepository == null)
                {
                    _flightRepository = new FlightRepository(_context);
                }
                return _flightRepository;
            }
        }

        public INotificationRepository Notifications
        {
            get
            {
                if (_notificationRepository == null)
                {
                    _notificationRepository = new NotificationRepository(_context);
                }
                return _notificationRepository;
            }
        }

        public IPassengerRepository Passengers
        {
            get
            {
                if (_passengerRepository == null)
                {
                    _passengerRepository = new PassengerRepository(_context);
                }
                return _passengerRepository;
            }
        }

        public void Save() => _context.SaveChanges();
    }
}
