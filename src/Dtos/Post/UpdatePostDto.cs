using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.Post
{
    /// <summary>
    /// Represents the data required to update an existing post.
    /// </summary>
    public class UpdatePostDto
    {
        /// <summary>
        /// The new title of the post.
        /// </summary>
        public string? Title { get; set; }

        /// <summary>
        /// The new content of the post.
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// The new category ID to which the post belongs.
        /// </summary>
        public int? CategoryId { get; set; }
    }
}