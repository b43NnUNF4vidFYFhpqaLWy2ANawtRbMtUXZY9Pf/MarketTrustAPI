using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Reputation;
using MarketTrustAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Controllers
{
    /// <summary>
    /// Controller for managing the reputation service.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ReputationController : ControllerBase
    {
        private readonly IReputationService reputationService;

        /// <summary>
        /// Constructs a new ReputationController.
        /// </summary>
        /// <param name="reputationService">The reputation service.</param>
        public ReputationController(IReputationService reputationService)
        {
            this.reputationService = reputationService;
        }

        /// <summary>
        /// Gets the global trust for a user.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve global trust for.</param>
        /// <returns>The global trust for the user, or a 404 Not Found if the user does not exist.</returns>
        [HttpGet("global/{userId}")]
        [ProducesResponseType(typeof(double), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetGlobalTrust(string userId)
        {
            double? globalTrust = await reputationService.GetGlobalTrustAsync(userId);

            if (globalTrust == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(globalTrust);
            }
        }

        /// <summary>
        /// Gets the personal trust that the requesting user has for another user.
        /// </summary>
        /// <param name="getPersonalTrustDto">The data required to get personal trust values.</param>
        /// <returns>The personal trust value, or a 401 Unauthorized if the user ID is not found, or a 404 Not Found if the user does not exist.</returns>
        [HttpGet("personal")]
        [Authorize]
        [ProducesResponseType(typeof(double), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPersonalTrust([FromQuery] GetPersonalTrustDto getPersonalTrustDto)
        {
            string? trustorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (trustorId == null)
            {
                return Unauthorized("User ID not found");
            }

            double? personalTrust = await reputationService.GetPersonalTrustAsync(trustorId, getPersonalTrustDto.TrusteeId, getPersonalTrustDto.D);

            if (personalTrust == null)
            {
                return NotFound("User not found");
            }
            else
            {
                return Ok(personalTrust);
            }
        }
    }
}