using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.SpatialIndexManager
{
    public interface IGeographicConverter
    {
        double MetersToDegreesLatitude(double meters, double latitude);
        double MetersToDegreesLongitude(double meters, double latitude);
        double GreatCircleDistance(double lat1, double lon1, double lat2, double lon2);
    }
}