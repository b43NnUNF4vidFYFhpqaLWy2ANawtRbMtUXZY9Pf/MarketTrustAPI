using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Interfaces;
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

        public async Task<bool> ExistAsync(int id)
        {
            return await _context.Categories.AnyAsync(category => category.Id == id);
        }
    }
}