using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.SpatialIndexManager
{
    /// <summary>
    /// Methods for converting meters to degrees latitude and longitude and calculating great-circle distance.
    /// Formulas are from:
    /// https://en.wikipedia.org/wiki/Geographic_coordinate_system#Length_of_a_degree
    /// https://www.movable-type.co.uk/scripts/latlong.html
    /// </summary>
    public class GeographicConverter : IGeographicConverter
    {
        private const double M1 = 111132.92;
        private const double M2 = -559.82;
        private const double M3 = 1.175;
        private const double M4 = -0.0023;
        private const double P1 = 111412.84;
        private const double P2 = -93.5;
        private const double P3 = 0.118;
        private const double D = 6371000;

        /// <inheritdoc />
        public double MetersToDegreesLatitude(double meters, double latitude)
        {
            double latRad = DegreesToRadians(latitude);
            double latlen = M1 + (M2 * Math.Cos(2 * latRad)) + (M3 * Math.Cos(4 * latRad)) + (M4 * Math.Cos(6 * latRad));
            return meters / latlen;
        }

        /// <inheritdoc />
        public double MetersToDegreesLongitude(double meters, double latitude)
        {
            double latRad = DegreesToRadians(latitude);
            double longlen = (P1 * Math.Cos(latRad)) + (P2 * Math.Cos(3 * latRad)) + (P3 * Math.Cos(5 * latRad));
            return meters / longlen;
        }

        /// <inheritdoc />
        public double GreatCircleDistance(double lat1, double lon1, double lat2, double lon2)
        {
            double dLat = DegreesToRadians(lat2 - lat1);
            double dLon = DegreesToRadians(lon2 - lon1);

            double a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                       Math.Cos(DegreesToRadians(lat1)) * Math.Cos(DegreesToRadians(lat2)) *
                       Math.Sin(dLon / 2) * Math.Sin(dLon / 2);

            double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return D * c;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}