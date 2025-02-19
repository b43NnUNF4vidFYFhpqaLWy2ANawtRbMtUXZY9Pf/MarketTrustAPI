using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.SpatialIndexManager
{
    public interface ISpatialIndexManager
    {
        public void Insert(ILocatable item);
        public void Remove(ILocatable item);
        public IList<ILocatable> GetPointsInRadius(Point center, double radiusInMeters);
    }
}