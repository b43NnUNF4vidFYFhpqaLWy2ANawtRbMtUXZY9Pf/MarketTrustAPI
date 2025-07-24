using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.SpatialIndexManager
{
    /// <summary>
    /// Interface for spatial indexing.
    /// </summary>
    /// <typeparam name="T">The type of items to be indexed</typeparam>
    public interface ISpatialIndexManager<T> where T : ILocatable
    {
        /// <summary>
        /// Inserts an item into the spatial index.
        /// </summary>
        /// <param name="item">The item to insert</param>
        public void Insert(T item);

        /// <summary>
        /// Removes an item from the spatial index.
        /// </summary>
        /// <param name="item">The item to remove</param>
        public void Remove(T item);

        /// <summary>
        /// Checks if the spatial index has been initialized.
        /// </summary>
        /// <returns>True if the spatial index is initialized, false otherwise.</returns>
        public bool IsInitialized();

        /// <summary>
        /// Initializes the spatial index with a collection of items.
        /// </summary>
        /// <param name="items">The initial items to populate the index</param>
        public void Initialize(IEnumerable<T> items);
        
        /// <summary>
        /// Gets all items within a specified radius from a center point.
        /// </summary>
        /// <param name="center">The center point from which to search</param>
        /// <param name="radiusInMeters">Radius in meters to search around the center point</param>
        /// <returns>A list of items within the specified radius</returns>
        public IList<T> GetPointsInRadius(Point center, double radiusInMeters);
    }
}