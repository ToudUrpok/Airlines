using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using System.Collections.Generic;
    
namespace SAMAirline.DataProvider.Repositories
{
    public class AirlineRepository : BaseRepository<Airline>, IAirlineRepository
    {
        public AirlineRepository(AirlineDB context) : base(context)
        {
        }
    }
}
