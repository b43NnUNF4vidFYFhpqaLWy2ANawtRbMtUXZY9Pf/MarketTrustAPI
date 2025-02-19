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
    }
}