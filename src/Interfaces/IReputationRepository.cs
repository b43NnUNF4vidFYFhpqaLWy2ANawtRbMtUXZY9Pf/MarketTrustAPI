using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;
using MarketTrustAPI.ReputationManager;

namespace MarketTrustAPI.Interfaces
{
    /// <summary>
    /// Represents the input data for reputation management algorithms.
    /// </summary>
    /// <remarks>
    /// The parameters correspond to the arguments of <see cref="IReputationManager.Update(Matrix{double}, bool[], Dictionary{string, int})"/> 
    /// </remarks>
    public record LocalTrustResult(Matrix<double> LocalTrust, bool[] Pretrusted, Dictionary<string, int> UserIdToTrustIndex);

    /// <summary>
    /// Interface for reputation repositories.
    /// </summary>
    public interface IReputationRepository
    {
        /// <summary>
        /// Retrives the trust data.
        /// </summary>
        /// <returns>The trust data</returns>
        public Task<LocalTrustResult> GetLocalTrustAsync();
    }
}