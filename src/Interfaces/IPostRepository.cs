using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Post;
using MarketTrustAPI.Dtos.PropertyValue;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    /// <summary>
    /// Interface for post repository.
    /// </summary>
    public interface IPostRepository
    {
        /// <summary>
        /// Retrieves all posts based on the specified filters.
        /// </summary>
        /// <param name="getPostDto">The filters for retrieving posts.</param>
        /// <returns>A list of posts matching the filters.</returns>
        Task<List<Post>> GetAllAsync(GetPostDto getPostDto);

        /// <summary>
        /// Retrieves a post by its ID.
        /// </summary>
        /// <param name="id">The ID of the post to retrieve.</param>
        /// <returns>The first post matching the ID, or null if not found.</returns>
        Task<Post?> GetByIdAsync(int id);

        /// <summary>
        /// Creates a new post.
        /// </summary>
        /// <param name="post">The post to create.</param>
        /// <returns>The created post.</returns>
        Task<Post> CreateAsync(Post post);

        /// <summary>
        /// Updates an existing post.
        /// </summary>
        /// <param name="id">The ID of the post to update.</param>
        /// <param name="updatePostDto">The new post data.</param>
        /// <returns>The updated post, or null if the post was not found.</returns>
        Task<Post?> UpdateAsync(int id, UpdatePostDto updatePostDto);

        /// <summary>
        /// Deletes a post with the specified ID.
        /// </summary>
        /// <param name="id">The ID of the post to delete.</param>
        /// <returns>The deleted post, or null if the post was not found.</returns>
        Task<Post?> DeleteAsync(int id);

        /// <summary>
        /// Adds a property value to a post.
        /// </summary>
        /// <param name="id">The ID of the post to add the property value to.</param>
        /// <param name="propertyValue">The property value to add.</param>
        /// <returns>The post with the added property value, or null if the post was not found.</returns>
        Task<Post?> AddPropertyValueAsync(int id, PropertyValue propertyValue);

        /// <summary>
        /// Updates a property value of a post.
        /// </summary>
        /// <param name="postId">The ID of the post containing the property value.</param>
        /// <param name="propertyValueId">The ID of the property value to update.</param>
        /// <param name="updatePropertyValueDto">The new property value data.</param>
        /// <returns>The post with the updated property value, or null if the post was not found.</returns>
        Task<Post?> UpdatePropertyValueAsync(int postId, int propertyValueId, UpdatePropertyValueDto updatePropertyValueDto);

        /// <summary>
        /// Deletes a property value from a post.
        /// </summary>
        /// <param name="postId">The ID of the post containing the property value.</param>
        /// <param name="propertyValueId">The ID of the property value to delete.</param>
        /// <returns>The post with the deleted property value, or null if the post was not found.</returns>
        Task<Post?> DeletePropertyValueAsync(int postId, int propertyValueId);

        /// <summary>
        /// Checks if a post with the specified ID exists.
        /// </summary>
        /// <param name="id">The ID of the post to check.</param>
        /// <returns>True if the post exists, otherwise false.</returns>
        Task<bool> ExistAsync(int id);

        /// <summary>
        /// Checks if a user owns a post.
        /// </summary>
        /// <param name="postId">The ID of the post to check.</param>
        /// <param name="userId">The ID of the user to check ownership for.</param>
        /// <returns>True if the user owns the post, otherwise false.</returns>
        Task<bool> UserOwnsPostAsync(int postId, string userId);

        /// <summary>
        /// Checks if a property name already exists for a post.
        /// </summary>
        /// <param name="postId">The ID of the post to check.</param>
        /// <param name="name">The name of the property to check.</param>
        /// <returns>True if the property name exists for the post, otherwise false.</returns>
        Task<bool> PropertyNameExistsAsync(int postId, string name);
    }
}