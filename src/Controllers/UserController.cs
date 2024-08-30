using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Dtos.User;
using MarketTrustAPI.Mappers;
using MarketTrustAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;

        public UserController(ApplicationDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            List<UserDto> users = _context.Users
                .Select(user => user.ToUserDto())
                .ToList();

            return Ok(users);
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] int id)
        {
            User? user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user.ToUserDto());
        }
            
        [HttpPost]
        public IActionResult Create([FromBody] CreateUserDto createUserDto)
        {
            User user = createUserDto.ToUserFromCreateDto();

            _context.Users.Add(user);
            _context.SaveChanges();

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, user.ToUserDto());
        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] int id, [FromBody] UpdateUserDto updateUserDto)
        {
            User? user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.IsPublicEmail = updateUserDto.IsPublicEmail;
            user.Phone = updateUserDto.Phone;
            user.IsPublicPhone = updateUserDto.IsPublicPhone;
            user.Location = updateUserDto.Location;
            user.IsPublicLocation = updateUserDto.IsPublicLocation;

            _context.SaveChanges();

            return Ok(user.ToUserDto());
        }

        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute] int id)
        {
            User? user = _context.Users.Find(id);

            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return NoContent();
        }
    }
}