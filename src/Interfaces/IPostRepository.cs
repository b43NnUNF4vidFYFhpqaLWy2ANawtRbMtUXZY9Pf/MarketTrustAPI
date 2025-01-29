using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Post;
using MarketTrustAPI.Models;

namespace MarketTrustAPI.Interfaces
{
    public interface IPostRepository
    {
        Task<List<Post>> GetAllAsync();
        Task<Post?> GetByIdAsync(int id);
        Task<Post> CreateAsync(Post post);
        Task<Post?> UpdateAsync(int id, UpdatePostDto updatePostDto);
        Task<Post?> DeleteAsync(int id);
    }
}