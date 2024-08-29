using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.HPRtree;

namespace MarketTrustAPI.SpatialIndexManager
{
    public class HPRtreeManager : ISpatialIndexManager
    {
        private readonly HPRtree<ILocatable> _hprTree;

        public HPRtreeManager(IEnumerable<ILocatable> items)
        {
            _hprTree = new HPRtree<ILocatable>();

            foreach (ILocatable item in items)
            {
                Point? location = item.GetLocation();
                if (location != null)
                {
                    _hprTree.Insert(location.EnvelopeInternal, item);
                }
            }
        }

        public IList<ILocatable> GetPointsInRadius(Point center, double radius)
        {
            Envelope box = new Envelope(
                center.Coordinate.X - radius, center.Coordinate.X + radius,
                center.Coordinate.Y - radius, center.Coordinate.Y + radius
            );

            List<ILocatable> nearby = _hprTree
                .Query(box)
                .Where(candidate => center.Distance(candidate.GetLocation()) <= radius)
                .ToList();

            return nearby;
        }
    }
}