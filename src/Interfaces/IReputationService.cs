using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MarketTrustAPI.ReputationManager;

namespace MarketTrustAPI.Interfaces
{
    /// <summary>
    /// Interface for reputation services.
    /// </summary>
    public interface IReputationService
    {
        /// <summary>
        /// Retrieves the trust data and updates the reputation manager.
        /// </summary>
        /// <returns>Null.</returns>
        public Task UpdateAsync();

        /// <summary>
        /// Retrieves the global trust value for a user.
        /// </summary>
        /// <param name="userId">The ID of the user.</param>
        /// <returns>The global trust value for the user, or null if the user does not exist.</returns>
        public Task<double?> GetGlobalTrustAsync(string userId);

        /// <summary>
        /// Retrieves the personal trust value from a trustor to a trustee.
        /// </summary>
        /// <param name="trustorId">The ID of the trustor.</param>
        /// <param name="trusteeId">The ID of the trustee.</param>
        /// <param name="d">See <see cref="IReputationManager.GetPersonalTrust(int, double)"/></param>
        /// <returns>The personal trust value from the trustor to the trustee, or null if either user does not exist.</returns>
        public Task<double?> GetPersonalTrustAsync(string trustorId, string trusteeId, double d);
    }
}