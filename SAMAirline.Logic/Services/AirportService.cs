using SAMAirline.DataProvider.Entities;
using SAMAirline.DataProvider.Interfaces;
using SAMAirline.Logic.Interfaces;
using SAMAirline.Model.Models;
using SAMAirline.Model.ModelsDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SAMAirline.Logic.Services
{
    public class AirportService : IAirportService
    {
        private readonly IAirportRepository _airportRepository;
        private readonly IMapper<AirportDto, Airport> _mapper;

        public AirportService(
            IAirportRepository airportRepository,
            IMapper<AirportDto, Airport> mapper)
        {
            _airportRepository = airportRepository;
            _mapper = mapper;
        }

        public IEnumerable<AirportDto> GetAll()
        {
            var list = _airportRepository.GetAll();

            var airports = list.Select(a => _mapper.MapToDtoEntity(a)).ToList();

            return airports;
        }
    }
}
