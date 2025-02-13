using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MarketTrustAPI.Interfaces
{

    public record LocalTrustResult(Matrix<double> LocalTrust, bool[] Pretrusted, Dictionary<string, int> UserIdToTrustIndex);

    public interface IReputationRepository
    {
        public Task<LocalTrustResult> GetLocalTrustAsync();
    }
}