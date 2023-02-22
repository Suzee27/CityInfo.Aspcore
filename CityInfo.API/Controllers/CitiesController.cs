using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Authorize]
    [Route("api")]
    public class CitiesController : ControllerBase
    {
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        }

        [HttpGet("cities")]
        public async Task<ActionResult<IEnumerable<CityWithoutPointsOfInteretDto>>> GetCities()
        {
            var cityEntities = await _cityInfoRepository.GetCitiesAsync();
            
            return Ok(_mapper.Map<IEnumerable<CityWithoutPointsOfInteretDto>>(cityEntities));
        }

        [HttpGet("city/{id}")]
        public async Task<IActionResult> GetCity(int id, bool includePointOfInterest = false)
        {
           var city =  await _cityInfoRepository.GetCityAsync(id, includePointOfInterest);
            if (city == null) return NotFound();

            if (includePointOfInterest)
            {
                return Ok(_mapper.Map<CityDto>(city));
            }

            return Ok(_mapper.Map<CityWithoutPointsOfInteretDto>(city));
        }
    }
}
