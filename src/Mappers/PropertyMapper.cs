using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Property;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Mappers
{
    public static class PropertyMapper
    {
        public static PropertyDto ToPropertyDto(this Property property)
        {
            return new PropertyDto
            {
                Id = property.Id,
                Name = property.Name,
                IsMandatory = property.IsMandatory,
                CategoryId = property.CategoryId
            };
        }
    }
}