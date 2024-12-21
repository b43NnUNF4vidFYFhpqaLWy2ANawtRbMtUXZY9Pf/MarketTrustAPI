using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MarketTrustAPI.ReputationManager
{
    public interface IReputationManager
    {
        public void Update();
        public Vector<double> GetGlobalTrust();
        public Vector<double> GetPersonalTrust(int i, double d);
    }
}