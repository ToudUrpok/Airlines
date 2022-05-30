using Autofac;
using Autofac.Core;
using Autofac.Integration.Mvc;
using System.Web.Mvc;
using SAMAirline.Logic.Services;
using SAMAirline.Logic.Interfaces;
using SAMAirline.DataProvider.Repositories;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider;
using SAMAirline.Logic.Mappers;
using SAMAirline.Model.ModelsDto;

namespace SAMAirline.Website.IoC
{
    public class AutofacConfig
    {
        public static void ConfigureContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).PropertiesAutowired();

            builder.RegisterType<AircraftMapper>()        .As<IMapper<AircraftDto, Aircraft>>();
            builder.RegisterType<AirlineMapper>()         .As<IMapper<AirlineDto, Airline>>();
            builder.RegisterType<AirportMapper>()         .As<IMapper<AirportDto, Airport>>();
            builder.RegisterType<BookingMapper>()         .As<IMapper<BookingDto, Booking>>();
            builder.RegisterType<FlightMapper>()          .As<IMapper<FlightDto, Flight>>();
            builder.RegisterType<NotificationMapper>()    .As<IMapper<NotificationDto, Notification>>();
            builder.RegisterType<UserMapper>()            .As<IMapper<UserDto, User>>();


            builder.RegisterType<AirlineService>()       .As<IAirlineService>();
            builder.RegisterType<AircraftService>()       .As<IAircraftService>();
            builder.RegisterType<AirportService>()        .As<IAirportService>();
            builder.RegisterType<BookingService>()        .As<IBookingService>();
            builder.RegisterType<FlightService>()         .As<IFlightService>();
            builder.RegisterType<NotificationService>()   .As<INotificationService>();
            builder.RegisterType<PassengerService>()      .As<IPassengerService>();
            builder.RegisterType<CommitService>()         .As<ICommitService>();

            builder.RegisterType<AirlineRepository>()    .As<IAirlineRepository>();
            builder.RegisterType<AircraftRepository>()    .As<IAircraftRepository>();
            builder.RegisterType<AirportRepository>()     .As<IAirportRepository>();
            builder.RegisterType<BookingRepository>()     .As<IBookingRepository>();
            builder.RegisterType<FlightRepository>()      .As<IFlightRepository>();
            builder.RegisterType<NotificationRepository>().As<INotificationRepository>();
            builder.RegisterType<PassengerRepository>()   .As<IPassengerRepository>();

            builder.RegisterType<UnitOfWork>()            .As<IUnitOfWork>();

            builder.RegisterType<AirlineDB>()             .InstancePerRequest();

            var container = builder.Build();

            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}