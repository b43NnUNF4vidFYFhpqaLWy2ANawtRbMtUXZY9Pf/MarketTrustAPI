using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.User;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    public interface IUserRepository
    {
        Task<List<User>> GetAllAsync(GetUserDto getUserDto);
        Task<User?> GetByIdAsync(string id);
        Task<User?> UpdateAsync(string id, UpdateUserDto updateUserDto);
        Task<User?> DeleteAsync(string id);
        Task<bool> ExistAsync(string id);
    }
}