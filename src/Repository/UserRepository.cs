using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Dtos.User;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Models;
using MarketTrustAPI.SpatialIndexManager;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace MarketTrustAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDBContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ISpatialIndexManager<User> _spatialIndexManager;

        public UserRepository(ApplicationDBContext context, UserManager<User> userManager, ISpatialIndexManager<User> spatialIndexManager)
        {
            _context = context;
            _userManager = userManager;
            _spatialIndexManager = spatialIndexManager;
        }

        public async Task<List<User>> GetAllAsync(GetUserDto getUserDto)
        {
            IQueryable<User> users = _context.Users.Include(user => user.Posts);

            if (!string.IsNullOrEmpty(getUserDto.Name))
            {
                users = users.Where(user => EF.Functions.Like(user.UserName, $"%{getUserDto.Name}%"));
            }

            if (!string.IsNullOrEmpty(getUserDto.Email))
            {
                users = users.Where(user => !user.IsPublicEmail || EF.Functions.Like(user.Email, $"%{getUserDto.Email}%"));
            }

            if (!string.IsNullOrEmpty(getUserDto.Phone))
            {
                users = users.Where(user => !user.IsPublicPhone || EF.Functions.Like(user.PhoneNumber, $"%{getUserDto.Phone}%"));
            }

            return await users.ToListAsync();
        }

        public async Task<User?> GetByIdAsync(string id) 
        {
            return await _context.Users
                .Include(user => user.Posts)
                .FirstOrDefaultAsync(user => user.Id == id);
        }

        public async Task<User?> UpdateAsync(string id, UpdateUserDto updateUserDto)
        {
            User? user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return null;
            }

            if (!string.IsNullOrEmpty(updateUserDto.Name))
            {
                IdentityResult result = await _userManager.SetUserNameAsync(user, updateUserDto.Name);

                if (!result.Succeeded)
                {
                    return null;
                }
            }

            if (!string.IsNullOrEmpty(updateUserDto.Email))
            {
                IdentityResult result = await _userManager.SetEmailAsync(user, updateUserDto.Email);

                if (!result.Succeeded)
                {
                    return null;
                }
            }

            user.IsPublicEmail = updateUserDto.IsPublicEmail ?? user.IsPublicEmail;
            user.PhoneNumber = updateUserDto.Phone ?? user.PhoneNumber;
            user.IsPublicPhone = updateUserDto.IsPublicPhone ?? user.IsPublicPhone;
            if (updateUserDto.Location != null)
            {
                user.Location = updateUserDto.Location;

                _spatialIndexManager.Remove(user);
                _spatialIndexManager.Insert(user);
            }
            user.IsPublicLocation = updateUserDto.IsPublicLocation ?? user.IsPublicLocation;

            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<User?> DeleteAsync(string id)
        {
            // Due to the delete behavior being client cascade,
            // the TrustRatings must be loaded too for EF Core
            User? user = await _context.Users
                .Include(user => user.TrustRatingsAsTrustor)
                .Include(user => user.TrustRatingsAsTrustee)
                .FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
            {
                return null;
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ExistAsync(string id)
        {
            return await _context.Users.AnyAsync(user => user.Id == id);
        }
    }
}