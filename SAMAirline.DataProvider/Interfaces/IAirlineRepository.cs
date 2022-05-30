using SAMAirline.DataProvider.Entities;
using System.Collections.Generic;

namespace SAMAirline.DataProvider.Interfaces
{
    public interface IAirlineRepository
    {
        IEnumerable<Airline> GetAll();
    }
}
