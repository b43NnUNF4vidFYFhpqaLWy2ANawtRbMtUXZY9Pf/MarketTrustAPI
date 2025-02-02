using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.User;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Mappers
{
    public static class UserMappers
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                Name = user.UserName ?? string.Empty,
                Email = user.IsPublicEmail ? user.Email : null,
                Phone = user.IsPublicPhone ? user.PhoneNumber : null,
                Location = user.IsPublicLocation ? user.Location : null,
                Posts = user.Posts.Select(post => post.ToPostDto()).ToList()
            };
        }
    }
}