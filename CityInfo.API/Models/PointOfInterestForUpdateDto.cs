﻿using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestForUpdateDto
    {
        [Required]

        [MaxLength(20)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(100)]
        public string? Description { get; set; }
    }
}
