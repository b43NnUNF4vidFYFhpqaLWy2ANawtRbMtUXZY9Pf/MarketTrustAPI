using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Category?> GetByIdAsync(int id);
        public Task<List<Category>> GetDescendantsAsync(int id);
        public Task<bool> ExistAsync(int id);
    }
}