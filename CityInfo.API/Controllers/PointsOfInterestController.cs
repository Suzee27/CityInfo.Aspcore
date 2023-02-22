using AutoMapper;
using CityInfo.API.Models;
using CityInfo.API.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/[controller]")]
    [Authorize(Policy = "MustBeFromParis")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly IMailService _mailService;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public PointsOfInterestController(IMailService mailService,
            ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PointOfInterestDto>>> GetPointsOfInterest(int cityId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            var pointsOfInterestForACity =await _cityInfoRepository.GetPointsOfInterestForCity(cityId);
            return Ok(_mapper.Map<IEnumerable<PointOfInterestDto>>(pointsOfInterestForACity));
        }

        [HttpGet("{pointOfInterestId}")]
        public async Task<ActionResult<PointOfInterestDto>> GetPointOfInterestId(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
               return NotFound();
            }

            var pointOfInterest =await _cityInfoRepository.GetPointsOfInterestForCity(cityId, pointOfInterestId);
            if (pointOfInterest == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<PointOfInterestDto>(pointOfInterest));
        }

        [HttpPost]
        public async Task<ActionResult<PointOfInterestDto>> CreatePointOfInterest(int cityId, PointOfInterestForCreationDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }
            var newPointOfInterest = _mapper.Map<Entities.PointOfInterest>(pointOfInterest);

            await _cityInfoRepository.AddPointOfInterestForCityAsync(cityId, newPointOfInterest);
            await _cityInfoRepository.SaveChangesAsync();

            var CreatedPointOfInterestToReturn = _mapper.Map<Models.PointOfInterestDto>(newPointOfInterest);
            return CreatedAtRoute("GetPointOfInterest",
                new
                {
                    cityId = cityId,
                    pointOfInterestId = CreatedPointOfInterestToReturn.Id
                }, CreatedPointOfInterestToReturn); 
        }

        [HttpPut("{pointOfInterestId}")]
        public async Task<ActionResult> UpadatePointOfInterest(int cityId, int pointOfInterestId, PointOfInterestForUpdateDto pointOfInterest)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfinterestEntity = await _cityInfoRepository.GetPointsOfInterestForCity
                (cityId, pointOfInterestId);
            if (pointOfinterestEntity == null) return NotFound();

            _mapper.Map(pointOfInterest, pointOfinterestEntity);
            await _cityInfoRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpDelete("{pointOfInterestId}")]
        public async Task<ActionResult> Delete(int cityId, int pointOfInterestId)
        {
            if (!await _cityInfoRepository.CityExistsAsync(cityId))
            {
                return NotFound();
            }

            var pointOfinterestEntity = await _cityInfoRepository.GetPointsOfInterestForCity(cityId, pointOfInterestId);
            if (pointOfinterestEntity == null) return NotFound();

            _cityInfoRepository.DeletePointOfInterest(pointOfinterestEntity);
            await _cityInfoRepository.SaveChangesAsync();

            _mailService.Send("point of interest deleted", $"point of interest {pointOfinterestEntity.Name} with id {pointOfinterestEntity.Id} was deleted");
            return NoContent();
        }
    }
}
