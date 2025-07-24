using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Post;
using NetTopologySuite.Geometries;

namespace MarketTrustAPI.Dtos.User
{
    /// <summary>
    /// DTO for <see cref="User"/> model.
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// The ID of the user.
        /// </summary>
        public string Id { get; set; } = string.Empty;

        /// <summary>
        /// The name of the user.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The email of the user.
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// The phone number of the user.
        /// </summary>
        public string? Phone { get; set; }

        /// <summary>
        /// The geographical location of the user.
        /// </summary>
        public Point? Location { get; set; }

        /// <summary>
        /// The posts created by the user.
        /// </summary>
        public List<PostDto> Posts { get; set; } = new List<PostDto>();
    }
}