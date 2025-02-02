using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Account;
using MarketTrustAPI.Dtos.User;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ITokenService _tokenService;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            User? user = await _userManager.FindByNameAsync(loginDto.Name);

            if (user == null)
            {
                return Unauthorized("Invalid username");
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, loginDto.Password, false);

            if (signInResult.Succeeded)
            {
                return Ok(
                    new NewUserDto
                    {
                        Name = user.UserName,
                        Email = user.Email,
                        Token = _tokenService.CreateToken(user)
                    }
                );
            }
            else
            {
                return Unauthorized("Invalid password");
            }
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
            try
            {
                User user = new()
                {
                    UserName = registerDto.UserName,
                    Email = registerDto.Email,
                    IsPublicEmail = registerDto.IsPublicEmail,
                    PhoneNumber = registerDto.Phone,
                    IsPublicPhone = registerDto.IsPublicPhone,
                    Location = registerDto.Location,
                    IsPublicLocation = registerDto.IsPublicLocation
                };

                IdentityResult createResult = await _userManager.CreateAsync(user, registerDto.Password);

                if (createResult.Succeeded)
                {
                   IdentityResult roleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (roleResult.Succeeded)
                    {
                        return Ok(
                            new NewUserDto
                            {
                                Name = user.UserName,
                                Email = user.Email,
                                Token = _tokenService.CreateToken(user)
                            }
                        );
                    }
                    else
                    {
                        return BadRequest(roleResult.Errors);
                    }
                }
                else
                {
                    return BadRequest(createResult.Errors);
                }
            }
            catch (Exception e) {
                return BadRequest(e.Message);
            }
        }
    }
}