using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using MarketTrustAPI.SpatialIndexManager;
using NetTopologySuite.Geometries;

namespace SpatialIndexManagerTests
{
    public class QuadtreeManagerTests
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
        public void GetPointsInRaadius_ReturnsCorrectPoints()
        {
            GeographicConverter geographicConverter = new GeographicConverter();
            QuadtreeManager<TestLocatable> manager = new QuadtreeManager<TestLocatable>(geographicConverter);

            Point center = new Point(0, 0);
            double radius = DegreeLatitudeInMeters;
            List<TestLocatable> items = new List<TestLocatable>
            {
                new TestLocatable(0, 0),
                new TestLocatable(2, 0),
            };

            foreach (var item in items)
            {
                manager.Insert(item);
            }

            IList<TestLocatable> result = manager.GetPointsInRadius(center, radius);

            Assert.Contains(items[0], result);
            Assert.DoesNotContain(items[1], result);
        }

        [Fact]
        public void GetPointsInRadius_ReturnsPointsOnBoundary()
        {
            GeographicConverter geographicConverter = new GeographicConverter();
            QuadtreeManager<TestLocatable> manager = new QuadtreeManager<TestLocatable>(geographicConverter);

            Point center = new Point(0, 0);
            double radius = DegreeLatitudeInMeters;
            List<TestLocatable> items = new List<TestLocatable>
            {
                new TestLocatable(1, 0),
                new TestLocatable(1.01, 0),
            };

            foreach (var item in items)
            {
                manager.Insert(item);
            }

            IList<TestLocatable> result = manager.GetPointsInRadius(center, radius);

            Assert.Contains(items[0], result);
            Assert.DoesNotContain(items[1], result);
        }
    }
}