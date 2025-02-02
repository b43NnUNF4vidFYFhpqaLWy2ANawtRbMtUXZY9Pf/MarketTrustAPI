using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.PropertyValue
{
    public class AddPropertyValueDto
    {
        [Required]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Value { get; set; } = string.Empty;
    }
}