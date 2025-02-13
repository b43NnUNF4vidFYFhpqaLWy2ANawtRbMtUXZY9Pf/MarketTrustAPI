using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MarketTrustAPI.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReputationController : ControllerBase
    {
        private readonly IReputationService reputationService;

        public ReputationController(IReputationService reputationService)
        {
            this.reputationService = reputationService;
        }

        [HttpGet("global/{userId}")]
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

        [HttpGet("personal/{trusteeId}/{d}")]
        [Authorize]
        public async Task<IActionResult> GetPersonalTrust(string trusteeId, double d)
        {
            if (d < 0 || d > 1)
            {
                return BadRequest("d must be between 0 and 1");
            }

            string? trustorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (trustorId == null)
            {
                return Unauthorized("User ID not found");
            }

            double? personalTrust = await reputationService.GetPersonalTrustAsync(trustorId, trusteeId, d);

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