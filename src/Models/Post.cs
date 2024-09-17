using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.SpatialIndexManager;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Models
{
    public class Post : ILocatable
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public List<PropertyValue> PropertyValues { get; set; } = new List<PropertyValue>();

        public Point? GetLocation()
        {
            return User.GetLocation();
        }
    }
}