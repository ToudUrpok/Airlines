using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System.Collections.Generic;

namespace SAMAirline.Logic.Interfaces
{
    public interface IAircraftService
    {
        IEnumerable<AircraftDto> GetAll();
    }
}
