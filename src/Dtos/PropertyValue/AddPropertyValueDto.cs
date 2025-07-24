using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.PropertyValue
{
    /// <summary>
    /// Represents the data required to add a new <see cref="PropertyValue"/> 
    /// </summary>
    public class AddPropertyValueDto
    {
        /// <summary>
        /// The name for the new property 
        /// </summary>
        [Required]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The value for the new property
        /// </summary>
        [Required]
        public string Value { get; set; } = string.Empty;
    }
}