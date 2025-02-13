using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Models;
using MathNet.Numerics.LinearAlgebra;
using Microsoft.EntityFrameworkCore;

namespace MarketTrustAPI.Repository
{
    public class ReputationRepository : IReputationRepository
    {
        private readonly ApplicationDBContext _context;

        public ReputationRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<LocalTrustResult> GetLocalTrustAsync()
        {
            List<User> users = await _context.Users.ToListAsync();
            Dictionary<string, int> userIdToTrustIndex = users
                .Select((user, index) => (user.Id, index))
                .ToDictionary(pair => pair.Id, pair => pair.index);
            Matrix<double> localTrust = Matrix<double>.Build.Dense(users.Count, users.Count);

            List<TrustRating> trustRatings = await _context.TrustRatings.ToListAsync();
            foreach (TrustRating trustRating in trustRatings)
            {
                bool trustorExists = userIdToTrustIndex.TryGetValue(trustRating.TrustorId, out int trustorIndex);
                bool trusteeExists = userIdToTrustIndex.TryGetValue(trustRating.TrusteeId, out int trusteeIndex);

                if (trustorExists && trusteeExists)
                {
                    localTrust[trustorIndex, trusteeIndex] += trustRating.TrustValue;
                }
            }

            bool[] pretrusted = users
                .Select(user => user.IsPretrusted)
                .ToArray();
            
            return new LocalTrustResult(localTrust, pretrusted, userIdToTrustIndex);
        }
    }
}