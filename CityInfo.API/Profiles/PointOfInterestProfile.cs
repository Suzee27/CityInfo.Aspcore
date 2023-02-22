using AutoMapper;

namespace CityInfo.API.Profiles
{
    public class PointOfInterestProfile: Profile
    {
        public PointOfInterestProfile()
        {
            // getting from the database
            CreateMap<Entities.PointOfInterest, Models.PointOfInterestDto>();

            // creating from the models and mapping it to the entities
            CreateMap<Models.PointOfInterestForCreationDto, Entities.PointOfInterest>();
            CreateMap<Models.PointOfInterestForUpdateDto, Entities.PointOfInterest>();
        }
    }
}
