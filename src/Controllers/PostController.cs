using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using MarketTrustAPI.Dtos.Post;
using MarketTrustAPI.Dtos.PropertyValue;
using MarketTrustAPI.Interfaces;
using MarketTrustAPI.Mappers;
using MarketTrustAPI.Models;
using Microsoft.AspNetCore.Authorization;
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
        private readonly IReputationService _reputationService;

        public PostController(IPostRepository postRepository, IUserRepository userRepository, ICategoryRepository categoryRepository, IReputationService reputationService)
        {
            _postRepository = postRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
            _reputationService = reputationService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetPostDto getPostDto)
        {
            // No authorization required, but if user is logged in, get their ID
            // to compute the personal trust of each of the posts
            string? trustorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<Post> posts = await _postRepository.GetAllAsync(getPostDto);
            List<PostDto> postDtos = new List<PostDto>(posts.Count);

            foreach (Post post in posts)
            {
                PostDto postDto = post.ToPostDto();
                postDto.GlobalTrust = await _reputationService.GetGlobalTrustAsync(post.UserId);
                postDto.PersonalTrust = trustorId != null && getPostDto.D.HasValue
                    ? await _reputationService.GetPersonalTrustAsync(trustorId, post.UserId, getPostDto.D.Value)
                    : null;

                postDtos.Add(postDto);
            }

            return Ok(postDtos);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id, [FromQuery] GetPostByIdDto getPostByIdDto)
        {
            string? trustorId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Post? post = await _postRepository.GetByIdAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            PostDto postDto = post.ToPostDto();
            postDto.GlobalTrust = await _reputationService.GetGlobalTrustAsync(post.UserId);
            postDto.PersonalTrust = trustorId != null && getPostByIdDto.D.HasValue
                ? await _reputationService.GetPersonalTrustAsync(trustorId, post.UserId, getPostByIdDto.D.Value)
                : null;

            return Ok(postDto);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create([FromBody] CreatePostDto createPostDto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (!await _userRepository.ExistAsync(userId))
            {
                return BadRequest("User does not exist");
            }

            if (!await _categoryRepository.ExistAsync(createPostDto.CategoryId))
            {
                return BadRequest("Category does not exist");
            }

            Post post = createPostDto.ToPostFromCreateDto(userId, createPostDto.CategoryId);

            await _postRepository.CreateAsync(post);

            return CreatedAtAction(nameof(GetById), new { id = post.Id }, post.ToPostDto());
        }

        [HttpPut("{id:int}")]
        [Authorize]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdatePostDto updatePostDto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (!await _postRepository.UserOwnsPostAsync(id, userId))
            {
                return Unauthorized("User is not the owner of the post");
            }

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
        [Authorize]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (!await _postRepository.UserOwnsPostAsync(id, userId))
            {
                return Unauthorized("User is not the owner of the post");
            }

            Post? post = await _postRepository.DeleteAsync(id);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            return Ok(post.ToPostDto());
        }

        [HttpPost("{id:int}")]
        [Authorize]
        public async Task<IActionResult> AddPropertyValue([FromRoute] int id, [FromBody] AddPropertyValueDto addPropertyValueDto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (!await _postRepository.UserOwnsPostAsync(id, userId))
            {
                return Unauthorized("User is not the owner of the post");
            }

            if (await _postRepository.PropertyNameExistsAsync(id, addPropertyValueDto.Name))
            {
                return BadRequest("Property name already exists");
            }

            PropertyValue propertyValue = addPropertyValueDto.ToPropertyValueFromAddDto(id);

            Post? post = await _postRepository.AddPropertyValueAsync(id, propertyValue);

            if (post == null)
            {
                return NotFound("Post not found");
            }

            return Ok(post.ToPostDto());
        }

        [HttpPut("{postId:int}/{propertyValueId:int}")]
        [Authorize]
        public async Task<IActionResult> UpdatePropertyValue([FromRoute] int postId, [FromRoute] int propertyValueId, [FromBody] UpdatePropertyValueDto updatePropertyValueDto)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (!await _postRepository.UserOwnsPostAsync(postId, userId))
            {
                return Unauthorized("User is not the owner of the post");
            }

            Post? post = await _postRepository.UpdatePropertyValueAsync(postId, propertyValueId, updatePropertyValueDto);

            if (post == null)
            {
                return NotFound("Post or property not found");
            }

            return Ok(post.ToPostDto());
        }

        [HttpDelete("{postId:int}/{propertyValueId:int}")]
        [Authorize]
        public async Task<IActionResult> DeletePropertyValue([FromRoute] int postId, [FromRoute] int propertyValueId)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return Unauthorized("User ID not found");
            }

            if (!await _postRepository.UserOwnsPostAsync(postId, userId))
            {
                return Unauthorized("User is not the owner of the post");
            }

            Post? post = await _postRepository.DeletePropertyValueAsync(postId, propertyValueId);

            if (post == null)
            {
                return NotFound("Post or property not found");
            }

            return Ok(post.ToPostDto());
        }
    }
}