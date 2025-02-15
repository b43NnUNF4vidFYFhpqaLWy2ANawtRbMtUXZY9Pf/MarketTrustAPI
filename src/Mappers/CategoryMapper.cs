using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Category;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Mappers
{
    public static class CategoryMapper
    {
        public static CategoryDto ToCategoryDto(this Category category)
        {
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Properties = category.Properties.Select(p => p.ToPropertyDto()).ToList(),
                ParentId = category.ParentId
            };
        }
    }
}