using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Models
{
    public class PropertyValue
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;

        public int PostId { get; set; }
        public Post Post { get; set; } = null!;
    }
}