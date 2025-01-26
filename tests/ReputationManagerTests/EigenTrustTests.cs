using System;
using System.Reflection;
using MathNet.Numerics.LinearAlgebra;
using MarketTrustAPI.ReputationManager;

namespace ReputationManagerTests
{
    public class EigenTrustTests
    {
        [Fact]
        public void GetGlobalTrust_IsTransitive()
        {
            Matrix<double> localTrust = Matrix<double>.Build.DenseOfArray(new double[,]
            {
                {0.0, 1.0, 0.0},
                {0.0, 0.0, 1.0},
                {0.0, 0.0, 0.0}
            });
            bool[] preTrusted = [false, true, false];
            double alpha = 0.5;
            double epsilon = 1e-3;
            int maxIterations = 1;
            EigenTrust eigenTrust = new EigenTrust(localTrust, preTrusted, alpha, epsilon, maxIterations);

            eigenTrust.Update();
            Vector<double> globalTrust = eigenTrust.GetGlobalTrust();
            
            Assert.Equal(0.0, globalTrust[0], 3);
            Assert.True(globalTrust[1] > 0.0);
            Assert.True(globalTrust[2] > 0.0);
        }
    }
}