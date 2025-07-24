using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Dtos.PropertyValue
{
    /// <summary>
    /// Represents the data required to update an existing property value.
    /// </summary>
    public class UpdatePropertyValueDto
    {
        /// <summary>
        /// The new name of the property value.
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// The new value of the property.
        /// </summary>
        public string? Value { get; set; }
    }
}