using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.TrustRating;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    /// <summary>
    /// Interface for trust rating repository.
    /// </summary>
    public interface ITrustRatingRepository
    {
        /// <summary>
        /// Retrieves all trust ratings based on the specified filters and the trustor's ID.
        /// </summary>
        /// <param name="getTrustRatingDto">The filters for retrieving trust ratings.</param>
        /// <param name="trustorId">The ID of the trustor to filter by.</param>
        /// <returns>A list of trust ratings matching the filters.</returns>
        public Task<List<TrustRating>> GetAllAsync(GetTrustRatingDto getTrustRatingDto, string trustorId);

        /// <summary>
        /// Retrieves a trust rating by its ID.
        /// </summary>
        /// <param name="id">The ID of the trust rating to retrieve.</param>
        /// <returns></returns>
        public Task<TrustRating?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new trust rating.
        /// </summary>
        /// <param name="trustRating">The trust rating to create.</param>
        /// <returns>The created trust rating.</returns>
        public Task<TrustRating> CreateAsync(TrustRating trustRating);

        /// <summary>
        /// Updates an existing trust rating.
        /// </summary>
        /// <param name="id">The ID of the trust rating to update.</param>
        /// <param name="updateTrustRatingDto">The new trust rating data.</param>
        /// <returns>The updated trust rating, or null if the trust rating was not found.</returns>
        public Task<TrustRating?> UpdateAsync(int id, UpdateTrustRatingDto updateTrustRatingDto);

        /// <summary>
        /// Deletes a trust rating with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the trust rating to delete.</param>
        /// <returns>The deleted trust rating, or null if the trust rating was not found.</returns>
        public Task<TrustRating?> DeleteAsync(int id);

        /// <summary>
        /// Checks if a user owns a trust rating.
        /// </summary>
        /// <param name="trustRatingId">The ID of the trust rating to check.</param>
        /// <param name="userId">The ID of the user to check ownership for.</param>
        /// <returns>True if the user owns the trust rating, otherwise false.</returns>
        public Task<bool> UserOwnsTrustRatingAsync(int trustRatingId, string userId);
    }
}