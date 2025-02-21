using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.TrustRating
{
    public class GetTrustRatingDto
    {
        public string? TrusteeId { get; set; }
        public int? PostId { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page number must be at least 1")]
        public int? Page { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Page size must be at least 1")]
        public int? PageSize { get; set; }
    }
}