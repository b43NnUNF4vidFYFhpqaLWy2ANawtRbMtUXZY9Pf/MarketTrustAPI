using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Dtos.TrustRating;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketTrustAPI.Repository
{
    public class TrustRatingRepository : ITrustRatingRepository
    {
        private readonly ApplicationDBContext _context;

        public TrustRatingRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<TrustRating>> GetAllAsync(GetTrustRatingDto getTrustRatingDto)
        {
            IQueryable<TrustRating> trustRatings = _context.TrustRatings.Include(tr => tr.Post);

            if (!string.IsNullOrEmpty(getTrustRatingDto.TrustorId))
            {
                trustRatings = trustRatings.Where(tr => tr.TrustorId == getTrustRatingDto.TrustorId);
            }

            if (!string.IsNullOrEmpty(getTrustRatingDto.TrusteeId))
            {
                trustRatings = trustRatings.Where(tr => tr.TrusteeId == getTrustRatingDto.TrusteeId);
            }

            if (getTrustRatingDto.PostId.HasValue)
            {
                trustRatings = trustRatings.Where(tr => tr.PostId == getTrustRatingDto.PostId);
            }

            if (getTrustRatingDto.Page.HasValue && getTrustRatingDto.PageSize.HasValue)
            {
                int skip = (getTrustRatingDto.Page.Value - 1) * getTrustRatingDto.PageSize.Value;
                trustRatings = trustRatings.Skip(skip).Take(getTrustRatingDto.PageSize.Value);
            }

            return await trustRatings.ToListAsync();
        }

        public async Task<TrustRating?> GetByIdAsync(int id)
        {
            return await _context.TrustRatings.FindAsync(id);
        }

        public async Task<TrustRating> CreateAsync(TrustRating trustRating)
        {
            await _context.TrustRatings.AddAsync(trustRating);
            await _context.SaveChangesAsync();

            return trustRating;
        }

        public async Task<TrustRating?> UpdateAsync(int id, UpdateTrustRatingDto updateTrustRatingDto)
        {
            TrustRating? trustRating = await _context.TrustRatings.FindAsync(id);

            if (trustRating == null)
            {
                return null;
            }

            trustRating.TrustValue = updateTrustRatingDto.TrustValue ?? trustRating.TrustValue;
            trustRating.Comment = updateTrustRatingDto.Comment ?? trustRating.Comment;

            await _context.SaveChangesAsync();

            return trustRating;
        }

        public async Task<TrustRating?> DeleteAsync(int id)
        {
            TrustRating? trustRating = await _context.TrustRatings.FindAsync(id);

            if (trustRating == null)
            {
                return null;
            }

            _context.TrustRatings.Remove(trustRating);
            await _context.SaveChangesAsync();

            return trustRating;
        }

        public async Task<bool> UserOwnsTrustRatingAsync(int trustRatingId, string userId)
        {
            TrustRating? trustRating = await _context.TrustRatings.FindAsync(trustRatingId);

            if (trustRating == null)
            {
                return false;
            }

            return trustRating.TrustorId == userId;
        }
    }
}