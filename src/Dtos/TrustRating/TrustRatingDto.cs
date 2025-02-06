using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.TrustRating
{
    public class TrustRatingDto
    {
        public int Id { get; set; }
        public string TrustorId { get; set; } = string.Empty;
        public string TrusteeId { get; set; } = string.Empty;
        public double TrustValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? PostId { get; set; }
        public string? Comment { get; set; }
    }
}