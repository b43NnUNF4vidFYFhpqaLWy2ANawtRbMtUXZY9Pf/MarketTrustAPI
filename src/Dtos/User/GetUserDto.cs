using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Dtos.User
{
    /// <summary>
    /// Represents the data required to search for users.
    /// </summary>
    [BindProperties]
    public class GetUserDto
    {
        /// <summary>
        /// The name of the users to search for.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The email of the users to search for.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The phone number of the users to search for.
        /// </summary>
        public string? Phone { get; set; }
    }
}