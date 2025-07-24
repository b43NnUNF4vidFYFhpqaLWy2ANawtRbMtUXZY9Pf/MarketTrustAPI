using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.User
{
    /// <summary>
    /// Represents the data required for user login.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// The name of the user logging in.
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The password of the user logging in.
        /// </summary>
        [Required]
        public string Password { get; set; } = string.Empty;
    }
}