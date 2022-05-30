using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System.Collections.Generic;
using System.Linq;

namespace SAMAirline.Logic.Services
{
    public class AircraftService : IAircraftService
    {
        private readonly IAircraftRepository _aircraftRepository;
        private readonly IMapper<AircraftDto, Aircraft> _mapper;

        public AircraftService(
            IAircraftRepository aircraftRepository,
            IMapper<AircraftDto, Aircraft> mapper)
        {
            _aircraftRepository = aircraftRepository;
            _mapper = mapper;
        }

        public IEnumerable<AircraftDto> GetAll()
        {
            var list = _aircraftRepository.GetAll();

            var aircrafts = list.Select(a => _mapper.MapToDtoEntity(a)).ToList();

            return aircrafts;
        }
    }
}
