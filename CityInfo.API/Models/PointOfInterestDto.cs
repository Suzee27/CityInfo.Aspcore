namespace CityInfo.API.Models
{
    public class PointOfInterestDto
    {
        public static int Count { get; internal set; }
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
    }
}
