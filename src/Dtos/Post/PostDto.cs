using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.PropertyValue;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Dtos.Post
{
    public class PostDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public double? GlobalTrust { get; set; }
        public double? PersonalTrust { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? LastUpdatedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public List<PropertyValueDto> PropertyValues { get; set; } = new List<PropertyValueDto>();
    }
}