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
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryController(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
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

        [HttpGet("{id:int}")]
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