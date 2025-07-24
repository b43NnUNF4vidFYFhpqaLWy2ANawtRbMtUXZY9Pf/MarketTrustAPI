using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Category;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    /// <summary>
    /// Interface for category repository.
    /// </summary>
    public interface ICategoryRepository
    {
        /// <summary>
        /// Retrieves all categories based on the specified filters.
        /// </summary>
        /// <param name="getCategoryDto">The filters for retrieving categories.</param>
        /// <returns>A list of categories matching the filters.</returns>
        public Task<List<Category>> GetAllAsync(GetCategoryDto getCategoryDto);

        /// <summary>
        /// Retrieves a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The first category matching the ID, or null if not found.</returns>
        public Task<Category?> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves all categories that are descendants of the specified category.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve descendants for.</param>
        /// <returns>A list of descendant categories.</returns>
        public Task<List<Category>> GetDescendantsAsync(int id);

        /// <summary>
        /// Checks if a category with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the category to check.</param>
        /// <returns>True if the category exists, otherwise false.</returns>
        public Task<bool> ExistAsync(int id);

        /// <summary>
        /// Retrieves all properties that are inherited from the ancesors of the specified category.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve inherited properties for.</param>
        /// <returns>A list of inherited properties.</returns>
        public Task<List<Property>> GetInheritedPropertiesAsync(int id);
    }
}