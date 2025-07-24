using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Models
{
    /// <summary>
    /// Represents a property and its value associated with a post.
    /// </summary>
    public class PropertyValue
    {
        /// <summary>
        /// The ID of the property value.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The value of the property.
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the post to which this value belongs.
        /// </summary>
        public int PostId { get; set; }

        /// <summary>
        /// The post to which this property value belongs.
        /// </summary>
        public Post Post { get; set; } = null!;
    }
}