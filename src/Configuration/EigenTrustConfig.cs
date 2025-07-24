using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.ReputationManager;

namespace MarketTrustAPI.Configuration
{
    /// <summary>
    /// Configuration options for the EigenTrust algorithm.
    /// </summary>
    /// <remarks>
    /// See <see cref="EigenTrust(double, double, int)"/> for parameter descriptions.
    /// </remarks>
    public class EigenTrustConfig
    {
        public double Alpha { get; set; }
        public double Epsilon { get; set; }
        public int MaxIterations { get; set; }
    }
}