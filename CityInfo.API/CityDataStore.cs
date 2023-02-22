using CityInfo.API.Models;

namespace CityInfo.API
{
    public class CityDataStore
    {
        public List<CityDto> Cities { get; set; }

        //public static CityDataStore Current { get; } = new CityDataStore(); 
        public CityDataStore()
        {
            Cities = new List<CityDto>()
            {
                new CityDto() { Id = 1,
                    Name ="Paris" ,
                    Description="beautiful city",
                     PointsOfInterests = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto()
                         {
                             Id = 1, Name = "Park", Description= "gjsjhd"
                         },
                         new PointOfInterestDto()
                         {
                             Id = 2, Name = "Pool", Description= "12gjsjhd"
                         }
                     }
                     },
                new CityDto() { 
                    Id = 2, Name = "France", Description ="lovely place",
                    PointsOfInterests = new List<PointOfInterestDto>()
                     {
                         new PointOfInterestDto()
                         {
                             Id = 1, Name = "Beach", Description= "gjsjhd"
                         },
                         new PointOfInterestDto()
                         {
                             Id = 2, Name = "church", Description= "12gjsjhd"
                         }
                     }
                }
            };
        }
    }
}
