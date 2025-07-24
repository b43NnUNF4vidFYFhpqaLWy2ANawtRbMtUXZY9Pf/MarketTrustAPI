using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.Property
{
    /// <summary>
    /// DTO for <see cref="Property"/> model.
    /// </summary>
    public class PropertyDto
    {
        /// <summary>
        /// The ID of the property.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the property.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Indicates whether the property is mandatory.
        /// </summary>
        public bool IsMandatory { get; set; }

        /// <summary>
        /// The ID of the category to which this property belongs.
        /// </summary>
        public int CategoryId { get; set; }
    }
}