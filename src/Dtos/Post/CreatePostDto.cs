using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.PropertyValue;

namespace MarketTrustAPI.Dtos.Post
{
    /// <summary>
    /// Represents the data required to create a new post.
    /// </summary>
    public class CreatePostDto
    {
        /// <summary>
        /// The title of the post.
        /// </summary>
        [Required]
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// The content of the post.
        /// </summary>
        public string Content { get; set; } = string.Empty;

        /// <summary>
        /// The category ID to which the post belongs.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }
    }
}