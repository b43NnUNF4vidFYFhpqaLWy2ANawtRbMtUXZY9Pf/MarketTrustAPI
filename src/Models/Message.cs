using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Models
{
    public class Message
    {
        public int Id { get; set; }
        public string Content { get; set; } = string.Empty;
        public DateTime SentAt { get; set; }

        public int ChatId { get; set; }
        public Chat Chat { get; set; } = null!;
        public int SenderId { get; set; }
        public User Sender { get; set; } = null!;
    }
}