using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MarketTrustAPI.ReputationManager;

namespace MarketTrustAPI.Dtos.Post
{
    /// <summary>
    /// Represents the data required when retrieving a post by its ID.
    /// </summary>
    [BindProperties]
    public class GetPostByIdDto
    {
        /// <summary>
        /// See <see cref="IReputationManager.GetPersonalTrust(int, double)"/> for details.
        /// </summary>
        [Range(0, 1, ErrorMessage = "d must be between 0 and 1")]
        public double? D { get; set; }
    }
}