using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.SpatialIndexManager;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Models
{
    public class User : ILocatable
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public bool IsPublicEmail { get; set; }
        public string? Phone { get; set; }
        public bool IsPublicPhone { get; set; }
        public Point? Location { get; set; }
        public bool IsPublicLocation { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();

        public Point? GetLocation()
        {
            return Location;
        }
    }
}