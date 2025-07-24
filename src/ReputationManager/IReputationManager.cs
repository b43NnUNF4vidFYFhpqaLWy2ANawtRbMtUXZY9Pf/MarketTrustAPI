using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MarketTrustAPI.ReputationManager
{
    /// <summary>
    /// Interface for reputation management algorithms.
    /// </summary>
    public interface IReputationManager
    {
        /// <summary>
        /// Checks if the reputation manager has been updated at least once.
        /// </summary>
        /// <returns>True if the reputation manager has been initialized, false otherwise.</returns>
        public bool IsInitialized();
        
        /// <summary>
        /// Update the global trust vector based on local trust values and pre-trusted users.
        /// </summary>
        /// <param name="localTrust">The local trust matrix.</param>
        /// <param name="pretrusted">The array indicating which users are pre-trusted.</param>
        /// <param name="userIdToTrustIndex">The mapping from user IDs to their corresponding index in the trust vector.</param>
        public void Update(Matrix<double> localTrust, bool[] pretrusted, Dictionary<string, int> userIdToTrustIndex);
        
        /// <summary>
        /// Gets the mapping from user IDs to their corresponding index in the trust vector.
        /// </summary>
        /// <returns>A dictionary mapping user IDs to their index in the trust vector.</returns>
        public Dictionary<string, int> GetUserIdToTrustIndex();
        
        /// <summary>
        /// Gets the global trust vector.
        /// </summary>
        /// <returns>The global trust vector.</returns>
        public Vector<double> GetGlobalTrust();
        
        /// <summary>
        /// Gets the personal trust vector for a user.
        /// </summary>
        /// <param name="i">The index of the user in the global trust vector.</param>
        /// <param name="d">The bias towards global trust values (if d = 1) vs own trust values (if d = 0). Must be in the range 0 ≤ d ≤ 1.</param>
        /// <returns>The personal trust vector for the user.</returns>
        public Vector<double> GetPersonalTrust(int i, double d);
    }
}