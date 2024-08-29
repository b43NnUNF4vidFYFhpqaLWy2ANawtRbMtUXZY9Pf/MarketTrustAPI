using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.SpatialIndexManager
{
    public interface ILocatable
    {
        Point? GetLocation();
    }
}