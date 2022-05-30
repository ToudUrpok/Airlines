using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System.Collections.Generic;
using System.Linq;

namespace SAMAirline.Logic.Services
{
    public class AirlineService : IAirlineService
    {
        private readonly IAirlineRepository _airlineRepository;
        private readonly IMapper<AirlineDto, Airline> _mapper;

        public AirlineService(
            IAirlineRepository airlineRepository,
            IMapper<AirlineDto, Airline> mapper)
        {
            _airlineRepository = airlineRepository;
            _mapper = mapper;
        }

        public IEnumerable<AirlineDto> GetAll()
        {
            var list = _airlineRepository.GetAll();

            var airlines = list.Select(a => _mapper.MapToDtoEntity(a)).ToList();

            return airlines;
        }
    }
}
