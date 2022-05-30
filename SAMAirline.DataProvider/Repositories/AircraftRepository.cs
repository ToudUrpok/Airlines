using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using System.Collections.Generic;
    
namespace SAMAirline.DataProvider.Repositories
{
    public class AircraftRepository : BaseRepository<Aircraft>, IAircraftRepository
    {
        public AircraftRepository(AirlineDB context) : base(context)
        {
        }
    }
}
