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
                Name = user.Name,
                Email = user.IsPublicEmail ? user.Email : null,
                Phone = user.IsPublicPhone ? user.Phone : null,
                Location = user.IsPublicLocation ? user.Location : null
            };
        }

        public static User ToUserFromCreateDto(this CreateUserDto createUserDto)
        {
            return new User
            {
                Name = createUserDto.Name,
                Email = createUserDto.Email,
                IsPublicEmail = createUserDto.IsPublicEmail,
                Phone = createUserDto.Phone,
                IsPublicPhone = createUserDto.IsPublicPhone,
                Location = createUserDto.Location,
                IsPublicLocation = createUserDto.IsPublicLocation
            };
        }
    }
}