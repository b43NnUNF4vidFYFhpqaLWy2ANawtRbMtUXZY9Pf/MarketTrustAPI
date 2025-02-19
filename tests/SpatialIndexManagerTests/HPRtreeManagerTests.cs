using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MarketTrustAPI.SpatialIndexManager;
using NetTopologySuite.Geometries;

namespace SpatialIndexManagerTests
{
    public class HPRtreeManagerTests
    {
        private const double DegreeLatitudeInMeters = 111320;

        private class TestLocatable : ILocatable
        {
            private readonly Point _location;

            public TestLocatable(double x, double y)
            {
                _location = new Point(x, y);
            }

            public Point? GetLocation()
            {
                return _location;
            }
        }

        [Fact]
        public void GetPointsInRadius_ReturnsCorrectPoints()
        {
            GeographicConverter geographicConverter = new GeographicConverter();
            HPRtreeManager manager = new HPRtreeManager(geographicConverter);

            Point center = new Point(0, 0);
            double radius = DegreeLatitudeInMeters;
            List<ILocatable> items = new List<ILocatable>
            {
                new TestLocatable(0, 0),
                new TestLocatable(2, 2),
            };

            foreach (var item in items)
            {
                manager.Insert(item);
            }

            IList<ILocatable> result = manager.GetPointsInRadius(center, radius);

            Assert.Contains(items[0], result);
            Assert.DoesNotContain(items[1], result);
        }

        [Fact]
        public void GetPointsInRadius_ReturnsPointsOnBoundary()
        {
            GeographicConverter geographicConverter = new GeographicConverter();
            HPRtreeManager manager = new HPRtreeManager(geographicConverter);

            Point center = new Point(0, 0);
            double radius = DegreeLatitudeInMeters;
            List<ILocatable> items = new List<ILocatable>
            {
                new TestLocatable(1, 1),
                new TestLocatable(1.01, 1.01),
            };

            foreach (var item in items)
            {
                manager.Insert(item);
            }

            IList<ILocatable> result = manager.GetPointsInRadius(center, radius);

            Assert.Contains(items[0], result);
            Assert.DoesNotContain(items[1], result);
        }
    }
}