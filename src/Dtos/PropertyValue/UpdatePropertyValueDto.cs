using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.PropertyValue
{
    public class UpdatePropertyValueDto
    {
        public string? Name { get; set; }
        public string? Value { get; set; }
    }
}