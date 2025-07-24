using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Category;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Mappers;
using MarketTrustAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Controllers
{
    /// <summary>
    /// Controller for managing categories.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        /// <summary>
        /// Constructs a new CategoryController.
        /// </summary>
        /// <param name="categoryRepository">The category repository.</param>
        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// Gets all categories based on the specified filters.
        /// </summary>
        /// <param name="getCategoryDto">The filters for retrieving categories.</param>
        /// <returns>A list of categories matching the filters.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<CategoryDto>), 200)]
        public async Task<IActionResult> GetAll([FromQuery] GetCategoryDto getCategoryDto)
        {
            List<Category> categories = await _categoryRepository.GetAllAsync(getCategoryDto);
            List<CategoryDto> categoryDtos = categories
                .Select(category => category.ToCategoryDto())
                .ToList();

            foreach (CategoryDto categoryDto in categoryDtos)
            {
                List<Property> inheritedProperties = await _categoryRepository.GetInheritedPropertiesAsync(categoryDto.Id);
                categoryDto.InheritedProperties = inheritedProperties
                    .Select(property => property.ToPropertyDto())
                    .ToList();
            }

            return Ok(categoryDtos);
        }

        /// <summary>
        /// Gets a category by its ID.
        /// </summary>
        /// <param name="id">The ID of the category to retrieve.</param>
        /// <returns>The category with the specified ID, or a 404 Not Found if the category does not exist.</returns>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(CategoryDto), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Category? category = await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            CategoryDto categoryDto = category.ToCategoryDto();
            List<Property> inheritedProperties = await _categoryRepository.GetInheritedPropertiesAsync(id);
            categoryDto.InheritedProperties = inheritedProperties
                .Select(property => property.ToPropertyDto())
                .ToList();

            return Ok(categoryDto);
        }
    }
}