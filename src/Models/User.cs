using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.SpatialIndexManager;
using Microsoft.AspNetCore.Identity;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Models
{
    /// <summary>
    /// Represents a user.
    /// </summary>
    public class User : IdentityUser, ILocatable
    {
        /// <summary>
        /// Indicates whether the user's email is publicly visible.
        /// </summary>
        public bool IsPublicEmail { get; set; }

        /// <summary>
        /// Indicates whether the user's phone number is publicly visible.
        /// </summary>
        public bool IsPublicPhone { get; set; }

        /// <summary>
        /// The geographical location of the user.
        /// </summary>
        public Point? Location { get; set; }
        
        /// <summary>
        /// Indicates whether the user's location is publicly visible.
        /// </summary>
        public bool IsPublicLocation { get; set; }

        /// <summary>
        /// Indicates whether the user is pre-trusted (known to be trusted).
        /// </summary>
        public bool IsPretrusted { get; set; } = false;

        /// <summary>
        /// The posts created by the user.
        /// </summary>
        public List<Post> Posts { get; set; } = new List<Post>();
        
        /// <summary>
        /// The trust ratings given to other users.
        /// </summary>
        public List<TrustRating> TrustRatingsAsTrustor { get; set; } = new List<TrustRating>();

        /// <summary>
        /// The trust ratings received from other users.
        /// </summary>
        public List<TrustRating> TrustRatingsAsTrustee { get; set; } = new List<TrustRating>();

        /// <summary>
        /// Gets the geographical location of the user.
        /// </summary>
        /// <returns>The user's location as a <see cref="Point"/>.</returns>
        public Point? GetLocation()
        {
            return Location;
        }
    }
}