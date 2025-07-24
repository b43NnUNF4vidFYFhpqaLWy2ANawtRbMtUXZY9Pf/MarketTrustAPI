using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Dtos.Account
{
    /// <summary>
    /// Represents the data required for user registration.
    /// </summary>
    public class RegisterDto
    {
        /// <summary>
        /// The username of the user.
        /// </summary>
        [Required]
        public string UserName { get; set; } = string.Empty;

        /// <summary>
        /// The email address of the user.
        /// </summary>
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the user's email should be publicly visible.
        /// </summary>
        public bool IsPublicEmail { get; set; } = false;
        
        /// <summary>
        /// The phone number of the user.
        /// </summary>
        [Phone]
        public string? Phone { get; set; }

        /// <summary>
        /// Indicates whether the user's phone number should be publicly visible.
        /// </summary>
        public bool IsPublicPhone { get; set; } = false;

        /// <summary>
        /// The geographical location of the user.
        /// </summary>
        public Point? Location { get; set; }

        /// <summary>
        /// Indicates whether the user's location should be publicly visible.
        /// </summary>
        public bool IsPublicLocation { get; set; } = false;

        /// <summary>
        /// The password for the user.
        /// </summary>
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; } = string.Empty;
    }
}