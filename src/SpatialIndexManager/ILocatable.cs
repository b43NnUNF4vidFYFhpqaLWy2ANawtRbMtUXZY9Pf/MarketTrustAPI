using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.SpatialIndexManager
{
    /// <summary>
    /// Represents an entity that has a geographical location.
    /// </summary>
    public interface ILocatable
    {
        /// <summary>
        /// Gets the location of the entity.
        /// </summary>
        /// <returns>The location as a <see cref="Point"/>.</returns>
        Point? GetLocation();
    }
}