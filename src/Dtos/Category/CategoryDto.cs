using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Property;

namespace MarketTrustAPI.Dtos.Category
{
    public class CategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<PropertyDto> Properties { get; set; } = new List<PropertyDto>();
        public List<PropertyDto> InheritedProperties { get; set; } = new List<PropertyDto>();
        public int? ParentId { get; set; }
    }
}