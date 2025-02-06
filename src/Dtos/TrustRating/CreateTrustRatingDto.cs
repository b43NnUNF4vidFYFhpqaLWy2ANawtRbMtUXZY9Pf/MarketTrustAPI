using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.TrustRating
{
    public class CreateTrustRatingDto
    {
        [Required]
        public string TrusteeId { get; set; } = string.Empty;
        [Required]
        public double TrustValue { get; set; }
        public int? PostId { get; set; }
        public string? Comment { get; set; }
    }
}