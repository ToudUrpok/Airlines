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
    public class AirlineMapper : IMapper<AirlineDto, Airline>
    {
        public Airline MapToDalEntity(AirlineDto dtoEntity)
        {
            return new Airline
            {
                AirlineId = dtoEntity.AirlineId,
                AirlineCodeIATA = dtoEntity.AirlineCodeIATA,
                AirlineCodeICAO = dtoEntity.AirlineCodeICAO,
                Name = dtoEntity.Name,
                Country = dtoEntity.Country
            };
        }

        public AirlineDto MapToDtoEntity(Airline dalEntity)
        {
            return new AirlineDto
            {
                AirlineId = dalEntity.AirlineId,
                AirlineCodeIATA = dalEntity.AirlineCodeIATA,
                AirlineCodeICAO = dalEntity.AirlineCodeICAO,
                Name = dalEntity.Name,
                Country = dalEntity.Country
            };
        }
    }
}
