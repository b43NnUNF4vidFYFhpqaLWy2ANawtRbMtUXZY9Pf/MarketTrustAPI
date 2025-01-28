using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Dtos.User;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketTrustAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;

        public UserRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(user => user.Posts)
                .ThenInclude(post => post.PropertyValues)
                .ToListAsync();
        }

        public async Task<User?> GetByIdAsync(int id) 
        {
            return await _context.Users
                .Include(user => user.Posts)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User> CreateAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> UpdateAsync(int id, UpdateUserDto updateUserDto)
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.IsPublicEmail = updateUserDto.IsPublicEmail;
            user.Phone = updateUserDto.Phone;
            user.IsPublicPhone = updateUserDto.IsPublicPhone;
            user.Location = updateUserDto.Location;
            user.IsPublicLocation = updateUserDto.IsPublicLocation;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> DeleteAsync(int id)
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }
    }
}