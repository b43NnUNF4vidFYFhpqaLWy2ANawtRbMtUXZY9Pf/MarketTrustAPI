using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.SpatialIndexManager;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Models
{
    public class User : IdentityUser, ILocatable
    {
        public bool IsPublicEmail { get; set; }
        public bool IsPublicPhone { get; set; }
        public Point? Location { get; set; }
        public bool IsPublicLocation { get; set; }
        public bool IsPretrusted { get; set; } = false;

        public List<Post> Posts { get; set; } = new List<Post>();
        public List<TrustRating> TrustRatingsAsTrustor { get; set; } = new List<TrustRating>();
        public List<TrustRating> TrustRatingsAsTrustee { get; set; } = new List<TrustRating>();

        public Point? GetLocation()
        {
            return Location;
        }
    }
}