using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MarketTrustAPI.Interfaces
{
    public interface IReputationService
    {
        public Task UpdateAsync();
        public Task<double?> GetGlobalTrustAsync(string userId);
        public Task<double?> GetPersonalTrustAsync(string trustorId, string trusteeId, double d);
    }
}