using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MarketTrustAPI.Models
{
    /// <summary>
    /// Represents a category with subcategories and properties.
    /// </summary>
    public class Category
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
        /// The ID of the parent category.
        /// If this is a root category, this will be null.
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// The parent category.
        /// If this is a root category, this will be null.
        /// </summary>
        public Category? Parent { get; set; }

        /// <summary>
        /// The child categories.
        /// </summary>
        public List<Category> Children { get; set; } = new List<Category>();

        /// <summary>
        /// The properties associated with this category.
        /// </summary>
        public List<Property> Properties { get; set; } = new List<Property>();
    }
}