using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Dtos.User
{
    public class UpdateUserDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public bool IsPublicEmail { get; set; }
        public string? Phone { get; set; }
        public bool IsPublicPhone { get; set; }
        public Point? Location { get; set; }
        public bool IsPublicLocation { get; set; }
    }
}