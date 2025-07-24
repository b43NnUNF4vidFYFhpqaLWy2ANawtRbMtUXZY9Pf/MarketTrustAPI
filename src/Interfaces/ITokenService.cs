using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    /// <summary>
    /// Interface for JWT token service.
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Creates a JWT token for the specified user.
        /// </summary>
        /// <param name="user">The user for whom the token is created.</param>
        /// <exception cref="ArgumentNullException">Thrown if the signing key is not configured.</exception>
        /// <returns>A JWT token.</returns>
        string CreateToken(User user);
    }
}