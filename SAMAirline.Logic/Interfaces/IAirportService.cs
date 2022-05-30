using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System.Collections.Generic;

namespace SAMAirline.Logic.Interfaces
{
    public interface IAirportService
    {
        IEnumerable<AirportDto> GetAll();
    }
}
