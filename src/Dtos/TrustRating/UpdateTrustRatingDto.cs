using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.TrustRating
{
    /// <summary>
    /// Represents the data required to update a trust rating.
    /// </summary>
    public class UpdateTrustRatingDto
    {
        /// <summary>
        /// The new trust value.
        /// </summary>
        public double? TrustValue { get; set; }

        /// <summary>
        /// The new comment for the trust rating.
        /// </summary>
        public string? Comment { get; set; }
    }
}