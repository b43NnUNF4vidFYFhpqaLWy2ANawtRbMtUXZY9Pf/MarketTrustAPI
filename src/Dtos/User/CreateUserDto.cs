using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Dtos.User
{
    public class CreateUserDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [EmailAddress]
        public string? Email { get; set; }
        [Required]
        public bool IsPublicEmail { get; set; }
        [Phone]
        public string? Phone { get; set; }
        [Required]
        public bool IsPublicPhone { get; set; }
        public Point? Location { get; set; }
        [Required]
        public bool IsPublicLocation { get; set; }
    }
}