using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.Property
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsMandatory { get; set; }
        public int CategoryId { get; set; }
    }
}