using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Configuration
{
    public class EigenTrustConfig
    {
        public double Alpha { get; set; }
        public double Epsilon { get; set; }
        public int MaxIterations { get; set; }
    }
}