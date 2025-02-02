using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Post;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Dtos.User
{
    public class UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public Point? Location { get; set; }
        public List<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}