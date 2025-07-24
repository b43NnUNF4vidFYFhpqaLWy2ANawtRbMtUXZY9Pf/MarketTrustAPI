using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MarketTrustAPI.Models
{
    /// <summary>
    /// Represents a property associated with a category.
    /// This is used to define the allowable properties for posts in the category.
    /// </summary>
    public class Property
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
        /// Indicates whether the property is mandatory for posts in the category.
        /// </summary>
        public bool IsMandatory { get; set; }

        /// <summary>
        /// The ID of the category to which this property belongs.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// The category to which this property belongs.
        /// </summary>
        public Category Category { get; set; } = null!;
    }
}