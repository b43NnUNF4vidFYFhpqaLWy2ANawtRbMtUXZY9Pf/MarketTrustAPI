using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Category;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<List<Category>> GetAllAsync(GetCategoryDto getCategoryDto);
        public Task<Category?> GetByIdAsync(int id);
        public Task<List<Category>> GetDescendantsAsync(int id);
        public Task<bool> ExistAsync(int id);
        public Task<List<Property>> GetInheritedPropertiesAsync(int id);
    }
}