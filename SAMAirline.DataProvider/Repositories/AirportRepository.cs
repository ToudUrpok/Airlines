using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using System.Collections.Generic;

namespace SAMAirline.DataProvider.Repositories
{
    public class AirportRepository : BaseRepository<Airport>, IAirportRepository
    {
        public AirportRepository(AirlineDB context) : base(context)
        {
        }
    }
}
