using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarketTrustAPI.ReputationManager;

namespace MarketTrustAPI.Dtos.Reputation
{
    /// <summary>
    /// Represents the data required to get personal trust values.
    /// </summary>
    [BindProperties]
    public class GetPersonalTrustDto
    {
        /// <summary>
        /// The ID of the user whose personal trust vector is being requested.
        /// </summary>
        [Required]
        public string TrusteeId { get; set; } = string.Empty;

        /// <summary>
        /// See <see cref="IReputationManager.GetPersonalTrust(int, double)"/> for details.
        /// </summary>
        [Required]
        [Range(0, 1, ErrorMessage = "d must be between 0 and 1")]
        public double D { get; set; }
    }
}