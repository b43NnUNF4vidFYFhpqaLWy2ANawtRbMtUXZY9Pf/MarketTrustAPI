using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.User
{
    /// <summary>
    /// Represents the data required to create a new user.
    /// </summary>
    public class NewUserDto
    {
        /// <summary>
        /// The name of the new user.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The email of the new user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The JWT token for the new user.
        /// </summary>
        public string Token { get; set; } = string.Empty;
    }
}