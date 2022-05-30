using SAMAirline.DataProvider.Entities;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Mappers
{
    public class AirportMapper : IMapper<AirportDto, Airport>
    {
        public Airport MapToDalEntity(AirportDto dtoEntity)
        {
            return new Airport
            {
                AirportId = dtoEntity.AirportId,
                AirportCodeIATA = dtoEntity.AirportCodeIATA,
                AirportCodeICAO = dtoEntity.AirportCodeICAO,
                Name = dtoEntity.Name,
                City = dtoEntity.City,
                Country = dtoEntity.Country
            };
        }

        public AirportDto MapToDtoEntity(Airport dalEntity)
        {
            return new AirportDto
            {
                AirportId = dalEntity.AirportId,
                AirportCodeIATA = dalEntity.AirportCodeIATA,
                AirportCodeICAO = dalEntity.AirportCodeICAO,
                Name = dalEntity.Name,
                City = dalEntity.City,
                Country = dalEntity.Country
            };
        }
    }
}
