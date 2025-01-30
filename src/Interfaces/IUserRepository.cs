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
        Task<User?> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task<User?> UpdateAsync(int id, UpdateUserDto updateUserDto);
        Task<User?> DeleteAsync(int id);
        Task<bool> ExistAsync(int id);
    }
}