using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Post;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Mappers
{
    public static class PostMapper
    {
        public static PostDto ToPostDto(this Post post)
        {
            return new PostDto
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                CreatedAt = post.CreatedAt,
                LastUpdatedAt = post.LastUpdatedAt,
                UserId = post.UserId,
                CategoryId = post.CategoryId,
                PropertyValues = post.PropertyValues.Select(pv => pv.ToPropertyValueDto()).ToList()
            };
        }
    }
}