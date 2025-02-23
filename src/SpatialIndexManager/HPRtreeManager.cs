using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.HPRtree;

namespace MarketTrustAPI.SpatialIndexManager
{
    public class HPRtreeManager<T> : ISpatialIndexManager<T> where T : ILocatable
    {
        private readonly HPRtree<T> _hprTree;
        private readonly IGeographicConverter _geographicConverter;
        private bool _isInitialized = false;

        public HPRtreeManager(IGeographicConverter geographicConverter)
        {
            _hprTree = new HPRtree<T>();
            _geographicConverter = geographicConverter;
        }

        public void Insert(T item)
        {
            Point? location = item.GetLocation();
            if (location != null)
            {
                _hprTree.Insert(location.EnvelopeInternal, item);
            }
        }

        public void Remove(T item)
        {
            Point? location = item.GetLocation();
            if (location != null)
            {
                _hprTree.Remove(location.EnvelopeInternal, item);
            }
        }

        public bool IsInitialized()
        {
            return _isInitialized;
        }

        public void Initialize(IEnumerable<T> items)
        {
            if (IsInitialized())
            {
                return;
            }

            foreach (T item in items)
            {
                Insert(item);
            }

            _isInitialized = true;
        }

        public IList<T> GetPointsInRadius(Point center, double radiusInMeters)
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