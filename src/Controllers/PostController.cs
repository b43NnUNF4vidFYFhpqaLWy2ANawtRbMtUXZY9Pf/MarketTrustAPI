using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Post;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Mappers;
using MarketTrustAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace MarketTrustAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostController : ControllerBase
    {
        private readonly IPostRepository _postRepository;
        private readonly IUserRepository _userRepository;
        private readonly ICategoryRepository _categoryRepository;

        public PostController(IPostRepository postRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetPostDto getPostDto)
        {
            List<Post> posts = await _postRepository.GetAllAsync(getPostDto);
            List<PostDto> postDtos = posts
                .Select(post => post.ToPostDto())
                .ToList();

            return Ok(postDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Post? post = await _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return Ok(post.ToPostDto());
        }

        [HttpPost("{userId:int}/{categoryId:int}")]
        public async Task<IActionResult> Create([FromRoute] int userId, [FromRoute] int categoryId, [FromBody] CreatePostDto createPostDto)
        {
            if (!await _userRepository.ExistAsync(userId))
            {
                return BadRequest("User does not exist");
            }

            if (!await _categoryRepository.ExistAsync(categoryId))
            {
                return BadRequest("Category does not exist");
            }

            Post post = createPostDto.ToPostFromCreateDto(userId, categoryId);

            await _postRepository.CreateAsync(post);

            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post.ToPostDto());
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostDto updatePostDto)
        {
            if (updatePostDto.CategoryId != null && !await _categoryRepository.ExistAsync(updatePostDto.CategoryId.Value))
            {
                return BadRequest("Category does not exist");
            }

            Post? post = await _postRepository.UpdateAsync(id, updatePostDto);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            return Ok(post.ToPostDto());
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            Post? post = await _postRepository.DeleteAsync(id);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            return Ok(post.ToPostDto());
        }
    }
}