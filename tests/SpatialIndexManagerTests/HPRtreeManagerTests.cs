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
            List<ILocatable> items = new List<ILocatable>
            {
                new TestLocatable(0, 0),
                new TestLocatable(1, 1),
                new TestLocatable(2, 2),
                new TestLocatable(5, 5)
            };
            ISpatialIndexManager manager = new HPRtreeManager(items);
            Point center = new Point(0, 0);
            double radius = 2.5;

            IList<ILocatable> result = manager.GetPointsInRadius(center, radius);

            Assert.Contains(items[0], result);
            Assert.Contains(items[1], result);
            Assert.DoesNotContain(items[2], result);
            Assert.DoesNotContain(items[3], result);
        }

        [Fact]
        public void GetPointsInRadius_ReturnsEmptyWhenNoPointsInRadius()
        {
            var items = new List<ILocatable>
            {
                new TestLocatable(10, 10),
                new TestLocatable(20, 20)
            };
            var manager = new HPRtreeManager(items);
            var center = new Point(0, 0);
            double radius = 5;

            var result = manager.GetPointsInRadius(center, radius);

            Assert.Empty(result);
        }
    }
}