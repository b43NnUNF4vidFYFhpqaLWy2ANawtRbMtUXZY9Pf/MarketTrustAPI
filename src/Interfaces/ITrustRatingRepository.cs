using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.TrustRating;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    public interface ITrustRatingRepository
    {
        public Task<List<TrustRating>> GetAllAsync(GetTrustRatingDto getTrustRatingDto);
        public Task<TrustRating?> GetByIdAsync(int id);
        public Task<TrustRating> CreateAsync(TrustRating trustRating);
        public Task<TrustRating?> UpdateAsync(int id, UpdateTrustRatingDto updateTrustRatingDto);
        public Task<TrustRating?> DeleteAsync(int id);
        public Task<bool> UserOwnsTrustRatingAsync(int trustRatingId, string userId);
    }
}