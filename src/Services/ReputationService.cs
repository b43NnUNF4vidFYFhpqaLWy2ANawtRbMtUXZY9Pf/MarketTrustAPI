using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.ReputationManager;
using MathNet.Numerics.LinearAlgebra;

namespace MarketTrustAPI.Services
{
    public class ReputationService : IReputationService
    {
        private readonly IReputationManager _reputationManager;
        private readonly IReputationRepository _reputationRepository;

        public ReputationService(IReputationManager reputationManager, IReputationRepository reputationRepository)
        {
            _reputationManager = reputationManager;
            _reputationRepository = reputationRepository;
        }

        public async Task UpdateAsync()
        {
            LocalTrustResult localTrustResult = await _reputationRepository.GetLocalTrustAsync();

            _reputationManager.Update(
                localTrustResult.LocalTrust,
                localTrustResult.Pretrusted,
                localTrustResult.UserIdToTrustIndex
            );
        }

        public async Task<double?> GetGlobalTrustAsync(string userId)
        {
            if (!_reputationManager.IsInitialized())
            {
                await UpdateAsync();
            }

            Vector<double> globalTrust = _reputationManager.GetGlobalTrust();
            Dictionary<string, int> userIdToTrustIndex = _reputationManager.GetUserIdToTrustIndex();
            
            if (userIdToTrustIndex.TryGetValue(userId, out int trustIndex))
            {
                return globalTrust[trustIndex];
            }
            else 
            {
                return null;
            }
        }

        public async Task<double?> GetPersonalTrustAsync(string trustorId, string trusteeId, double d)
        {
            if (!_reputationManager.IsInitialized())
            {
                await UpdateAsync();
            }

            Dictionary<string, int> userIdToTrustIndex = _reputationManager.GetUserIdToTrustIndex();
            if (userIdToTrustIndex.TryGetValue(trustorId, out int trustorIndex) &&
                userIdToTrustIndex.TryGetValue(trusteeId, out int trusteeIndex))
            {
                Vector<double> personalTrust = _reputationManager.GetPersonalTrust(trustorIndex, d);

                return personalTrust[trusteeIndex];
            }
            else
            {
                return null;
            }
        }
    }
}