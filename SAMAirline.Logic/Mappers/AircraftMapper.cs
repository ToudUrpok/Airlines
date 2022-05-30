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
    public class AircraftMapper : IMapper<AircraftDto, Aircraft>
    {
        public Aircraft MapToDalEntity(AircraftDto dtoEntity)
        {
            return new Aircraft
            {
                AircraftId = dtoEntity.AircraftId,
                AircraftCodeIATA = dtoEntity.AircraftCodeIATA,
                AircraftCodeICAO = dtoEntity.AircraftCodeICAO,
                Model = dtoEntity.Model,
                TotalPlaces = dtoEntity.TotalPlaces
            };
        }

        public AircraftDto MapToDtoEntity(Aircraft dalEntity)
        {
            return new AircraftDto
            {
                AircraftId = dalEntity.AircraftId,
                AircraftCodeIATA = dalEntity.AircraftCodeIATA,
                AircraftCodeICAO = dalEntity.AircraftCodeICAO,
                Model = dalEntity.Model,
                TotalPlaces = dalEntity.TotalPlaces
            };
        }
    }
}
