using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.SpatialIndexManager
{
    /// <summary>
    /// Interface for geographic conversion operations.
    /// </summary>
    public interface IGeographicConverter
    {
        /// <summary>
        /// Converts a distance in meters to degrees latitude at a given latitude.
        /// </summary>
        /// <param name="meters">The distance in meters to convert.</param>
        /// <param name="latitude">The latitude at which to perform the conversion.</param>
        /// <returns>The equivalent distance in degrees latitude.</returns>
        double MetersToDegreesLatitude(double meters, double latitude);

        /// <summary>
        /// Converts a distance in meters to degrees longitude at a given latitude.
        /// </summary>
        /// <param name="meters">The distance in meters to convert.</param>
        /// <param name="latitude"> The latitude at which to perform the conversion.</param>
        /// <returns>The equivalent distance in degrees longitude.</returns>
        double MetersToDegreesLongitude(double meters, double latitude);

        /// <summary>
        /// Calculates the great-circle distance between two points specified by latitude and longitude.
        /// </summary>
        /// <param name="lat1">The latitude of the first point.</param>
        /// <param name="lon1">The longitude of the first point.</param>
        /// <param name="lat2">The latitude of the second point.</param>
        /// <param name="lon2">The longitude of the second point.</param>
        /// <returns>The great-circle distance in meters.</returns>
        double GreatCircleDistance(double lat1, double lon1, double lat2, double lon2);
    }
}