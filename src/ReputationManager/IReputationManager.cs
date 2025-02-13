using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MarketTrustAPI.ReputationManager
{
    public interface IReputationManager
    {
        public bool IsInitialized();
        public void Update(Matrix<double> localTrust, bool[] pretrusted, Dictionary<string, int> userIdToTrustIndex);
        public Dictionary<string, int> GetUserIdToTrustIndex();
        public Vector<double> GetGlobalTrust();
        public Vector<double> GetPersonalTrust(int i, double d);
    }
}