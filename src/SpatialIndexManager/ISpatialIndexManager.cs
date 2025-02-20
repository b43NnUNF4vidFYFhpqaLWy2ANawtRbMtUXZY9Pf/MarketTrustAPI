using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.SpatialIndexManager
{
    public interface ISpatialIndexManager<T> where T : ILocatable
    {
        public void Insert(T item);
        public void Remove(T item);
        public bool IsInitialized();
        public void Initialize(IEnumerable<T> items);
        public IList<T> GetPointsInRadius(Point center, double radiusInMeters);
    }
}