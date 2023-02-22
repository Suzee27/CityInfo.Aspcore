using CityInfo.API.Entities;
using Microsoft.EntityFrameworkCore;

namespace CityInfo.API.DbContexts
{
    public class CityInfoContext: DbContext
    {
        public CityInfoContext(DbContextOptions<CityInfoContext> option)
            : base(option)
        {

        }

        public DbSet<City> Cities { get; set; } = null;
        public DbSet<PointOfInterest> PointOfInterest { get; set; }= null;
    
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().HasData(
                new City("Paris")
                {
                    Id = 1,
                    Description = "beautiful city",
                },
                new City("France")
                {
                    Id = 2,
                    Description = "lovely place"
                });

            modelBuilder.Entity<PointOfInterest>().HasData(
                new PointOfInterest("Park")
                {
                    Id = 1,
                    CityId = 1,
                    Description = "gjsjhd"
                },
                new PointOfInterest("Pool")
                {
                    Id = 2,
                    CityId= 1,
                    Description = "12gjsjhd"
                },
                new PointOfInterest("Beach")
                {
                    Id = 3,
                    CityId = 2,
                    Description = "gjsjhd"
                },
                new PointOfInterest("church")
                {
                    Id = 4,
                    CityId = 2,
                    Description = "12gjsjhd"
                });
            base.OnModelCreating(modelBuilder);
        }
        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlite("connectionstring");
        //    base.OnConfiguring(optionsBuilder);
        //}
    }
}
