using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;
using NetTopologySuite.Index.Quadtree;

namespace MarketTrustAPI.SpatialIndexManager
{
    public class QuadtreeManager<T> : ISpatialIndexManager<T> where T : ILocatable
    {
        private readonly Quadtree<T> _quadTree;
        private readonly IGeographicConverter _geographicConverter;
        private bool _isInitialized = false;

        /// <summary>
        /// Construct a new QuadtreeManager.
        /// </summary>
        /// <param name="geographicConverter">The geographic converter</param>
        public QuadtreeManager(IGeographicConverter geographicConverter)
        {
            _quadTree = new Quadtree<T>();

            _geographicConverter = geographicConverter;
        }

        /// <inheritdoc />
        public void Insert(T item)
        {
            Point? location = item.GetLocation();
            if (location != null)
            {
                _quadTree.Insert(location.EnvelopeInternal, item);
            }
        }

        /// <inheritdoc />
        public void Remove(T item)
        {
            Point? location = item.GetLocation();
            if (location != null)
            {
                _quadTree.Remove(location.EnvelopeInternal, item);
            }
        }

        /// <inheritdoc />
        public bool IsInitialized()
        {
            return _isInitialized;
        }

        /// <inheritdoc />
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

        /// <inheritdoc />
        public IList<T> GetPointsInRadius(Point center, double radiusInMeters)
        {
            double latitude = center.Coordinate.Y;
            double radiusInDegreesLat = _geographicConverter.MetersToDegreesLatitude(radiusInMeters, latitude);
            double radiusInDegreesLon = _geographicConverter.MetersToDegreesLongitude(radiusInMeters, latitude);

            Envelope box = new Envelope(
                center.Coordinate.X - radiusInDegreesLon, center.Coordinate.X + radiusInDegreesLon,
                center.Coordinate.Y - radiusInDegreesLat, center.Coordinate.Y + radiusInDegreesLat
            );

            IList<T> pointsInRadius = _quadTree
                .Query(box)
                .Where(point =>
                {
                    Point? location = point.GetLocation();
                    if (location == null)
                    {
                        return false;
                    }

                    double distance = _geographicConverter.GreatCircleDistance(
                        center.Coordinate.Y, center.Coordinate.X,
                        location.Coordinate.Y, location.Coordinate.X
                    );

                    return distance <= radiusInMeters;
                })
                .ToList();

            return pointsInRadius;
        }
    }
}