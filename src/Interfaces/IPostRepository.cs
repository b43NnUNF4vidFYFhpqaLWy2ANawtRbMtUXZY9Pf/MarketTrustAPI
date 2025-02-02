using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Post;
using MarketTrustAPI.Dtos.PropertyValue;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync(GetPostDto getPostDto);
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post post);
        Task<Post?> UpdateAsync(int id, UpdatePostDto updatePostDto);
        Task<Post?> DeleteAsync(int id);
        Task<Post?> AddPropertyValueAsync(int id, PropertyValue propertyValue);
        Task<Post?> UpdatePropertyValueAsync(int postId, int propertyValueId, UpdatePropertyValueDto updatePropertyValueDto);
        Task<Post?> DeletePropertyValueAsync(int postId, int propertyValueId);
        Task<bool> ExistAsync(int id);
        Task<bool> UserOwnsPostAsync(int postId, string userId);
        Task<bool> PropertyNameExistsAsync(int postId, string name);
    }
}