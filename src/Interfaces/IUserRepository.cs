using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.User;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    /// <summary>
    /// Interface for user repository.
    /// </summary>
    public interface IUserRepository
    {
        /// <summary>
        /// Retrieves all users based on the specified filters.
        /// </summary>
        /// <param name="getUserDto">The filters for retrieving users.</param>
        /// <returns>A list of users matching the filters.</returns>
        Task<List<User>> GetAllAsync(GetUserDto getUserDto);
        
        /// <summary>
        /// Retrieves a user by their ID.
        /// </summary>
        /// <param name="id">The ID of the user to retrieve.</param>
        /// <returns>The first user matching the ID, or null if not found.</returns>
        Task<User?> GetByIdAsync(string id);

        /// <summary>
        /// Updates a user with the specified ID
        /// </summary>
        /// <param name="id">The ID of the user to update.</param>
        /// <param name="updateUserDto">The new user data.</param>
        /// <returns>>The updated user, or null if the user was not found.</returns>
        Task<User?> UpdateAsync(string id, UpdateUserDto updateUserDto);

        /// <summary>
        /// Deletes a user with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the user to delete.</param>
        /// <returns>>The deleted user, or null if the user was not found.</returns>
        Task<User?> DeleteAsync(string id);

        /// <summary>
        /// Checks if a user with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the user to check.</param>
        /// <returns>>True if the user exists, otherwise false.</returns>
        Task<bool> ExistAsync(string id);
    }
}