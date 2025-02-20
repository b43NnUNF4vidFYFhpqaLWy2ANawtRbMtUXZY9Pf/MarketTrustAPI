using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.TrustRating;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Mappers;
using MarketTrustAPI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TrustRatingController : ControllerBase
    {
        private readonly ITrustRatingRepository _trustRatingRepository;
        private readonly IUserRepository _userRepository;
        private readonly IPostRepository _postRepository;

        public TrustRatingController(ITrustRatingRepository trustRatingRepository, IUserRepository userRepository, IPostRepository postRepository)
        {
            _trustRatingRepository = trustRatingRepository;
            _userRepository = userRepository;
            _postRepository = postRepository;
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreateTrustRatingDto createTrustRatingDto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (createTrustRatingDto.TrusteeId == userId)
            {
                return BadRequest("User cannot rate self");
            }

            if (!await _userRepository.ExistAsync(createTrustRatingDto.TrusteeId))
            {
                return NotFound("Trustee not found");
            }

            if (createTrustRatingDto.PostId != null && !await _postRepository.ExistAsync(createTrustRatingDto.PostId.Value))
            {
                return NotFound("Post not found");
            }

            TrustRating trustRating = createTrustRatingDto.ToTrustRatingFromCreateDto(userId);

            await _trustRatingRepository.CreateAsync(trustRating);

            return Ok(trustRating.ToTrustRatingDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTrustRatingDto updateTrustRatingDto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (!await _trustRatingRepository.UserOwnsTrustRatingAsync(id, userId))
            {
                return Unauthorized("User is not the owner of the trust rating or the trust rating does not exist");
            }

            TrustRating? trustRating = await _trustRatingRepository.UpdateAsync(id, updateTrustRatingDto);

            if (trustRating == null)
            {
                return NotFound("Trust rating not found");
            }

            return Ok(trustRating.ToTrustRatingDto());
        }

        [HttpDelete("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (!await _trustRatingRepository.UserOwnsTrustRatingAsync(id, userId))
            {
                return Unauthorized("User is not the owner of the trust rating or the trust rating does not exist");
            }

            TrustRating? trustRating = await _trustRatingRepository.DeleteAsync(id);

            if (trustRating == null)
            {
                return NotFound("Trust rating not found");
            }

            return Ok(trustRating.ToTrustRatingDto());
        }
    }
}