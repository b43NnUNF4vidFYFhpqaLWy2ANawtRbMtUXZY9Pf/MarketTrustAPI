using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Dtos.Category;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketTrustAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        /// <summary>
        /// Constructs a new CategoryRepository.
        /// </summary>
        /// <param name="context">The database context.</param>
        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        /// <inheritdoc />
        public async Task<List<Category>> GetAllAsync(GetCategoryDto getCategoryDto)
        {
            IQueryable<Category> categories = _context.Categories.Include(category => category.Properties);

            if (!string.IsNullOrEmpty(getCategoryDto.Name))
            {
                categories = categories.Where(category => EF.Functions.Like(category.Name, $"%{getCategoryDto.Name}%"));
            }

            return await categories.ToListAsync();
        }
        
        /// <inheritdoc />
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(category => category.Properties)
                .FirstOrDefaultAsync(category => category.Id == id);
        }

        /// <inheritdoc />
        public async Task<List<Category>> GetDescendantsAsync(int id)
        {
            Category? category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return [];
            }
            else
            {
                List<Category> descendants = new List<Category>();

                await LoadDescendantsAsync(category, descendants);

                return descendants;
            }
        }

        /// <inheritdoc />
        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(category => category.Id == id);
        }

        /// <inheritdoc />
        public async Task<List<Property>> GetInheritedPropertiesAsync(int id)
        {
            List<Property> inheritedProperties = new List<Property>();
            Category? category = await _context.Categories.FindAsync(id);

            while (category?.ParentId != null)
            {
                category = await _context.Categories
                    .Include(category => category.Properties)
                    .FirstOrDefaultAsync(c => c.Id == category.ParentId);
                
                if (category != null)
                {
                    inheritedProperties.AddRange(category.Properties);
                }
            }

            return inheritedProperties;
        }

        private async Task LoadDescendantsAsync(Category category, List<Category> descendants)
        {
            List<Category> children = await _context.Categories
                .Where(child => child.ParentId == category.Id)
                .Include(child => child.Children)
                .Include(child => child.Properties)
                .ToListAsync();

            foreach (Category child in children)
            {
                descendants.Add(child);
                await LoadDescendantsAsync(child, descendants);
            }
        }
    }
}