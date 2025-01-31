using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Data;
using MarketTrustAPI.Dtos.Post;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace MarketTrustAPI.Repository
{
    public class PostRepository : IPostRepository
    {
        private readonly ApplicationDBContext _context;

        public PostRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<List<Post>> GetAllAsync(GetPostDto getPostDto)
        {
            IQueryable<Post> posts = _context.Posts.Include(post => post.PropertyValues);

            if (!string.IsNullOrEmpty(getPostDto.Title))
            {
                posts = posts.Where(post => EF.Functions.Like(post.Title, $"%{getPostDto.Title}%"));
            }

            if (!string.IsNullOrEmpty(getPostDto.Content))
            {
                posts = posts.Where(post => 
                    EF.Functions.Like(post.Content, $"%{getPostDto.Content}%") || 
                    post.PropertyValues.Any(propertyValue => 
                        EF.Functions.Like(propertyValue.Value, $"%{getPostDto.Content}%")
                    )
                );
            }

            if (getPostDto.Page.HasValue && getPostDto.PageSize.HasValue)
            {
                int skip = (getPostDto.Page.Value - 1) * getPostDto.PageSize.Value;
                posts = posts.Skip(skip).Take(getPostDto.PageSize.Value);
            }

            return await posts.ToListAsync();
        }

        public async Task<Post?> GetByIdAsync(int id)
        {
            return await _context.Posts
                .Include(post => post.PropertyValues)
                .FirstOrDefaultAsync(post => post.Id == id);
        }

        public async Task<Post> CreateAsync(Post post)
        {
            await _context.Posts.AddAsync(post);
            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Post?> UpdateAsync(int id, UpdatePostDto updatePostDto)
        {
            Post? post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return null;
            }

            post.Title = updatePostDto.Title ?? post.Title;
            post.Content = updatePostDto.Content ?? post.Content;
            post.LastUpdatedAt = DateTime.Now;
            post.CategoryId = updatePostDto.CategoryId ?? post.CategoryId;

            await _context.SaveChangesAsync();

            return post;
        }

        public async Task<Post?> DeleteAsync(int id)
        {
            Post? post = await _context.Posts.FindAsync(id);

            if (post == null)
            {
                return null;
            }

            _context.Posts.Remove(post);
            await _context.SaveChangesAsync();

            return post;
        }
    }
}