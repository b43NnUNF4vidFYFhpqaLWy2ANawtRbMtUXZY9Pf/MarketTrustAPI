using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathNet.Numerics.LinearAlgebra;

namespace MarketTrustAPI.ReputationManager
{
    public class EigenTrust : IReputationManager
    {
        private readonly Matrix<double> _localTrust;
        private Matrix<double>? _normalizedLocalTrust;
        private readonly Vector<double> _preTrust;
        private Vector<double>? _globalTrust;
        private readonly double _alpha;
        private readonly double _epsilon;
        private readonly int _maxIterations;

        public EigenTrust(Matrix<double> localTrust, bool[] preTrusted, double alpha, double epsilon, int maxIterations)
        {
            if (localTrust.RowCount != localTrust.ColumnCount)
            {
                throw new ArgumentException("localTrust must be a square matrix");
            }

            if (localTrust.RowCount != preTrusted.Length)
            {
                throw new ArgumentException("preTrusted must have the same length as the rows of localTrust");
            }

            if (alpha <= 0 || alpha > 1)
            {
                throw new ArgumentException("alpha must be in the range (0, 1]");
            }

            if (epsilon < 0)
            {
                throw new ArgumentException("epsilon must be non-negative");
            }

            if (maxIterations <= 0)
            {
                throw new ArgumentException("maxIterations must be strictly positive");
            }

            _localTrust = localTrust;
            int preTrustedCount = preTrusted.Count(x => x);
            _preTrust = Vector<double>.Build.Dense(localTrust.RowCount, i => preTrusted[i] ? 1.0 / preTrustedCount : 0.0);
            _alpha = alpha;
            _epsilon = epsilon;
            _maxIterations = maxIterations;
        }

        public void Update()
        {
            UpdateNormalizedLocalTrust();
            UpdateGlobalTrust();
        }

        public Vector<double> GetGlobalTrust()
        {
            if (_globalTrust == null)
            {
                throw new InvalidOperationException("globalTrust must be set before calling GetGlobalTrust");
            }

            return _globalTrust;
        }

        public Vector<double> GetPersonalTrust(int i, double d)
        {
            if (_globalTrust == null)
            {
                throw new InvalidOperationException("globalTrust must be set before calling GetPersonalTrust");
            }

            if (_normalizedLocalTrust == null)
            {
                throw new InvalidOperationException("normalizedLocalTrust must be set before calling GetPersonalTrust");
            }

            if (i < 0 || i >= _localTrust.RowCount)
            {
                throw new ArgumentException("i must be in the range [0, localTrust.RowCount)");
            }

            if (d < 0 || d > 1)
            {
                throw new ArgumentException("d must be in the range [0, 1]");
            }

            // \vec{t_{personal}} = d \vec{t} + (1 - d) \vec{c}
            return d * _globalTrust + (1 - d) * _normalizedLocalTrust.Row(i);
        }

        private void UpdateNormalizedLocalTrust()
        {
            // c_{ij} = \frac{\max(s_{ij}, 0)}{\sum_j max(s_{ij}, 0)}
            Matrix<double> c = Matrix<double>.Build.Dense(_localTrust.RowCount, _localTrust.ColumnCount);
            for (int i = 0; i < _localTrust.RowCount; i++)
            {
                Vector<double> row = _localTrust.Row(i);
                double sum = row
                    .Where(x => x > 0)
                    .Sum();

                for (int j = 0; j < _localTrust.ColumnCount; j++)
                {
                    if (sum == 0)
                    {
                        c[i, j] = _preTrust[j];
                    }
                    else
                    {
                        c[i, j] = row[j] > 0 ? row[j] / sum : 0;
                    }
                }
            }

            _normalizedLocalTrust = c;
        }

        private void UpdateGlobalTrust()
        {
            if (_normalizedLocalTrust == null)
            {
                throw new InvalidOperationException("normalizedLocalTrust must be set before calling UpdateGlobalTrust");
            }

            Vector<double> t = _preTrust;
            // (1 - \alpha) C^T
            Matrix<double> weighted_C_transpose = (1 - _alpha) * _normalizedLocalTrust.Transpose();

            for (int i = 0; i < _maxIterations; i++)
            {
                Vector<double> t_new = weighted_C_transpose * t + _alpha * _preTrust;
                double delta = (t_new - t).L2Norm();
                t = t_new;

                if (delta < _epsilon)
                {
                    break;
                }
            }

            _globalTrust = t;
        }
    }
}