using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketTrustAPI.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDBContext _context;

        public CategoryRepository(ApplicationDBContext context)
        {
            _context = context;
        }
        
        public async Task<Category?> GetByIdAsync(int id)
        {
            return await _context.Categories
                .Include(category => category.Children)
                .Include(category => category.Properties)
                .FirstOrDefaultAsync(category => category.Id == id);
        }

        public async Task<List<Category>> GetDescendantsAsync(int id)
        {
            Category? category = await GetByIdAsync(id);

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

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(category => category.Id == id);
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