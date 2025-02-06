using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Models
{
    public class TrustRating
    {
        public int Id { get; set; }
        public string TrustorId { get; set; } = string.Empty;
        public User Trustor { get; set; } = null!;
        public string TrusteeId { get; set; } = string.Empty;
        public User Trustee { get; set; } = null!;
        public double TrustValue { get; set; }
        public DateTime CreatedAt { get; set; }
        public int? PostId { get; set; }
        public Post? Post { get; set; }
        public string? Comment { get; set; }
    }
}