using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Property;

namespace MarketTrustAPI.Dtos.Category
{
    /// <summary>
    /// DTO for <see cref="Category"/> model.
    /// </summary>
    public class CategoryDto
    {
        /// <summary>
        /// The ID of the category.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the category.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// The properties associated with the category.
        /// </summary>
        public List<PropertyDto> Properties { get; set; } = new List<PropertyDto>();

        /// <summary>
        /// The properties inherited from the parent(s) of the category.
        /// </summary>
        public List<PropertyDto> InheritedProperties { get; set; } = new List<PropertyDto>();

        /// <summary>
        /// The ID of the parent category.
        /// </summary>
        public int? ParentId { get; set; }
    }
}