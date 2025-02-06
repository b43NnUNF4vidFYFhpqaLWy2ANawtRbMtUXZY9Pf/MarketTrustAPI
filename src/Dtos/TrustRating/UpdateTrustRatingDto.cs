using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.TrustRating
{
    public class UpdateTrustRatingDto
    {
        public double? TrustValue { get; set; }
        public string? Comment { get; set; }
    }
}