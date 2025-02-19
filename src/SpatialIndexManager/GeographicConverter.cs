using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.SpatialIndexManager
{
    /// <summary>
    /// Converts meters to degrees latitude and longitude.
    /// Formulas are from:
    /// https://en.wikipedia.org/wiki/Geographic_coordinate_system#Length_of_a_degree
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

        public double MetersToDegreesLatitude(double meters, double latitude)
        {
            double latRad = DegreesToRadians(latitude);
            double latlen = M1 + (M2 * Math.Cos(2 * latRad)) + (M3 * Math.Cos(4 * latRad)) + (M4 * Math.Cos(6 * latRad));
            return meters / latlen;
        }

        public double MetersToDegreesLongitude(double meters, double latitude)
        {
            double latRad = DegreesToRadians(latitude);
            double longlen = (P1 * Math.Cos(latRad)) + (P2 * Math.Cos(3 * latRad)) + (P3 * Math.Cos(5 * latRad));
            return meters / longlen;
        }

        private static double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
    }
}