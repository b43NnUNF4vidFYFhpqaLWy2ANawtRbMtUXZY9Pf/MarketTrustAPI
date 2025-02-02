using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.PropertyValue;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Mappers
{
    public static class PropertyValueMapper
    {
        public static PropertyValueDto ToPropertyValueDto(this PropertyValue propertyValue)
        {
            return new PropertyValueDto
            {
                Id = propertyValue.Id,
                Name = propertyValue.Name,
                Value = propertyValue.Value,
                PostId = propertyValue.PostId
            };
        }

        public static PropertyValue ToPropertyValueFromAddDto(this AddPropertyValueDto addPropertyValueDto, int postId)
        {
            return new PropertyValue
            {
                Name = addPropertyValueDto.Name,
                Value = addPropertyValueDto.Value,
                PostId = postId
            };
        }
    }
}