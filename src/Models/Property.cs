using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MarketTrustAPI.Models
{
    public class Property
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool IsMandatory { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
    }
}