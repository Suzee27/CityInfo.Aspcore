using CityInfo.API.DbContexts;
using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.Service
{
    public class CityInfoRepository : ICityInfoRepository
    {
        private readonly CityInfoContext _context;

        public CityInfoRepository(CityInfoContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<City>> GetCitiesAsync()
        {
            return await _context.Cities.OrderBy(x => x.Name).ToListAsync();
        }

        public async Task<City?> GetCityAsync(int cityId, bool includePointsOfInterest)
        {
            if (includePointsOfInterest)
            {
                return await _context.Cities.Include(x => x.PointsOfInterests).Where(
                    x => x.Id == cityId).FirstOrDefaultAsync();
            }
            return await _context.Cities.Where(x =>x.Id == cityId).FirstOrDefaultAsync();
        }

        public async Task<bool> CityExistsAsync(int cityId)
        {
            return await _context.Cities.AnyAsync(c =>c.Id == cityId);
        }

        public async Task<IEnumerable<PointOfInterest>> GetPointsOfInterestForCity(int cityId)
        {
            return await _context.PointOfInterest.Where(x => x.CityId == cityId).ToListAsync();
        }

        public async Task<PointOfInterest?> GetPointsOfInterestForCity(int cityId, int pointOfInterest)
        {
            return await _context.PointOfInterest.Where(x =>x.CityId == cityId && x.Id == pointOfInterest).FirstOrDefaultAsync();
        }

        public async Task AddPointOfInterestForCityAsync(int cityId, PointOfInterest pointOfInterest)
        {
            var city = await GetCityAsync(cityId, false);
            if (city != null)
            {
                city.PointsOfInterests.Add(pointOfInterest);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync() >= 0);
        }

        public void DeletePointOfInterest(PointOfInterest pointOfInterest)
        {
            _context.PointOfInterest.Remove(pointOfInterest);
        }
    }
}
