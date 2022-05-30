using SAMAirline.DataProvider.Entities;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.ModelsDto;
using System;

namespace SAMAirline.Logic.Mappers
{
    public class FlightMapper : IMapper<FlightDto, Flight>
    {
        public Flight MapToDalEntity(FlightDto dtoEntity)
        {
            return new Flight
            {
                FlightId = dtoEntity.FlightId,
                FlightNumber = dtoEntity.FlightNumber,
                AirlineCode = dtoEntity.AirlineCode,
                DepartureDateTime = dtoEntity.DepartureDateTime,
                DepartureAirportCode = dtoEntity.DepAirportCode,
                AircraftCode = dtoEntity.AircraftCode,
                TotalPlaces = dtoEntity.FreePlaces,
                ArrivalDateTime = dtoEntity.ArrivalDateTime,
                ArrivalAirportCode = dtoEntity.ArrAirportCode,
                Stops = dtoEntity.Stops,
                Price = dtoEntity.Price
            };
        }

        public FlightDto MapToDtoEntity(Flight dalEntity)
        {
            return new FlightDto
            {
                FlightId = dalEntity.FlightId,
                FlightNumber = dalEntity.FlightNumber,
                AirlineCode = dalEntity.AirlineCode,
                AirlineName = dalEntity.Airline.Name,

                DepartureDateTime = dalEntity.DepartureDateTime,
                DepAirportCode = dalEntity.DepAirport.AirportCodeIATA,
                DepAirportName = dalEntity.DepAirport.Name,
                DepartureCity = dalEntity.DepAirport.City,
                DepartureCountry = dalEntity.DepAirport.Country,

                AircraftCode = dalEntity.Aircraft.AircraftCodeIATA,
                AircraftName = dalEntity.Aircraft.Model,
                FreePlaces = dalEntity.TotalPlaces,

                ArrivalDateTime = dalEntity.ArrivalDateTime,
                ArrAirportCode = dalEntity.ArrAirport.AirportCodeIATA,
                ArrivalCity = dalEntity.ArrAirport.AirportCodeIATA,
                ArrAirportName = dalEntity.ArrAirport.Name,
                ArrivalCountry = dalEntity.ArrAirport.Country,

                Price = dalEntity.Price
            };
        }
    }
}
