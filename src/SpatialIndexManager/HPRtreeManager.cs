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
        private readonly IGeographicConverter _geographicConverter;

        public HPRtreeManager(IGeographicConverter geographicConverter)
        {
            _hprTree = new HPRtree<ILocatable>();
            _geographicConverter = geographicConverter;
        }

        public void Insert(ILocatable item)
        {
            Point? location = item.GetLocation();
            if (location != null)
            {
                _hprTree.Insert(location.EnvelopeInternal, item);
            }
        }

        public void Remove(ILocatable item)
        {
            Point? location = item.GetLocation();
            if (location != null)
            {
                _hprTree.Remove(location.EnvelopeInternal, item);
            }
        }

        public IList<ILocatable> GetPointsInRadius(Point center, double radiusInMeters)
        {
            double latitude = center.Coordinate.Y;
            double radiusInDegreesLat = _geographicConverter.MetersToDegreesLatitude(radiusInMeters, latitude);
            double radiusInDegreesLon = _geographicConverter.MetersToDegreesLongitude(radiusInMeters, latitude);

            Envelope box = new Envelope(
                center.Coordinate.X - radiusInDegreesLon, center.Coordinate.X + radiusInDegreesLon,
                center.Coordinate.Y - radiusInDegreesLat, center.Coordinate.Y + radiusInDegreesLat
            );

            // NOTE: Secondary circular filter omitted for efficiency
            return _hprTree.Query(box);
        }
    }
}