using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Dtos.User;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Mappers;
using MarketTrustAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MarketTrustAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetUserDto getUserDto)
        {
            List<User> users = await _userRepository.GetAllAsync(getUserDto);
            List<UserDto> userDtos = users
                .Select(user => user.ToUserDto())
                .ToList();

            return Ok(userDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            User? user = await _userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDto());
        }
            
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto updateUserDto)
        {
            string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return Unauthorized("User ID not found");
            }

            User? user = await _userRepository.UpdateAsync(id, updateUserDto);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user.ToUserDto());
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> Delete()
        {
            string? id = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
            {
                return Unauthorized("User ID not found");
            }

            User? user = await _userRepository.DeleteAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}