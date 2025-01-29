using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.PropertyValue;

namespace MarketTrustAPI.Dtos.Post
{
    public class CreatePostDto
    {
        public string Title { get; set; } = string.Empty;
        public string? Content { get; set; }
    }
}