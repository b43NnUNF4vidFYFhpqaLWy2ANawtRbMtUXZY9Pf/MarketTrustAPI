using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.PropertyValue
{
    /// <summary>
    /// DTO for <see cref="PropertyValue"/> model.
    /// </summary>
    public class PropertyValueDto
    {
        /// <summary>
        /// The ID of the property value.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the property value.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The value of the property.
        /// </summary>
        public string Value { get; set; } = string.Empty;

        /// <summary>
        /// The ID of the property to which this value belongs.
        /// </summary>
        public int PostId { get; set; }
    }
}