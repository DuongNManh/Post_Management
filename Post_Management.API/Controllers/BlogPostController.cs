using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post_Management.API.CustomActionFilters;
using Post_Management.API.Exceptions;
using Post_Management.API.Models;
using Post_Management.API.Models.Domains;
using Post_Management.API.Models.DTOs;
using Post_Management.API.Models.Responses;
using Post_Management.API.Repositories;

namespace Post_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;
        private readonly ICategoryRepository _categoryRepository;

        public BlogPostController(IBlogPostRepository blogPostRepository, IMapper mapper, ICategoryRepository categoryRepository)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
            _categoryRepository = categoryRepository;
        }

        //Get:
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var blogPosts = await _blogPostRepository.GetAllBlogPosts();
            var paginatedResult = blogPosts.Select(blogPost => _mapper.Map<BlogPostResponse>(blogPost));
            var response = ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                message: "Success",
                data: paginatedResult
                );
            return Ok(response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var blogPost = await _blogPostRepository.GetBlogPostById(id);
                if (blogPost == null)
                {
                    throw new NotFoundException("Blog post not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: StatusCodes.Status200OK,
                    message: "Success",
                    data: _mapper.Map<BlogPostResponse>(blogPost)
                    );
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request", ex);
            }
        }

        //Post: /api/BlogPost
        [HttpPost]
        [ValidateModelAttributes]
        public async Task<IActionResult> Post([FromBody] BlogPostDTO valueDTO)
        {
            try
            {
                // Validate categories before creating the blog post
                if (valueDTO.Categories != null && valueDTO.Categories.Any())
                {
                    var categoriesExist = ValidateCategories(valueDTO.Categories).Result;
                    if (!categoriesExist)
                    {
                        throw new BadRequestException("One or more category IDs are invalid");
                    }
                }

                var blogPost = _mapper.Map<BlogPost>(valueDTO);

                // Fetch and assign categories after mapping
                if (valueDTO.Categories != null)
                {
                    blogPost.Categories = (await _categoryRepository.GetCategoriesByIds(valueDTO.Categories)).ToList();
                }

                var createdBlogPost = await _blogPostRepository.CreateBlogPost(blogPost);
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: StatusCodes.Status201Created,
                    message: "Blog post created successfully",
                    data: _mapper.Map<BlogPostResponse>(createdBlogPost)
                    );
                return CreatedAtAction(nameof(Get),
                    new { id = createdBlogPost.Id },
                    response);
            }
            catch (BadRequestException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request", ex);
            }
        }

        //Put: /api/BlogPost/{id}
        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModelAttributes]
        public async Task<IActionResult> Put(Guid id, BlogPostDTO blogPostDTO)
        {
            try
            {
                // Validate categories before creating the blog post
                if (blogPostDTO.Categories != null && blogPostDTO.Categories.Any())
                {
                    var categoriesExist = ValidateCategories(blogPostDTO.Categories).Result;
                    if (!categoriesExist)
                    {
                        throw new BadRequestException("One or more category IDs are invalid");
                    }
                }

                var updatedBlogPost = _mapper.Map<BlogPost>(blogPostDTO);

                // Fetch and assign categories after mapping
                if (blogPostDTO.Categories != null)
                {
                    updatedBlogPost.Categories = (await _categoryRepository.GetCategoriesByIds(blogPostDTO.Categories)).ToList();
                }

                var createdBlogPost = await _blogPostRepository.UpdateBlogPost(id, updatedBlogPost);

                if (createdBlogPost == null)
                {
                    throw new NotFoundException("Blog post not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: StatusCodes.Status200OK,
                    message: "Blog post updated successfully",
                    data: _mapper.Map<BlogPostResponse>(createdBlogPost)
                    );
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request", ex);
            }
        }

        //Delete: /api/BlogPost/{id}
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var deletedBlogPost = await _blogPostRepository.DeleteBlogPost(id);
                if (deletedBlogPost == null)
                {
                    throw new NotFoundException("Blog post not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: StatusCodes.Status200OK,
                    message: "Blog post deleted successfully",
                    data: _mapper.Map<BlogPostResponse>(deletedBlogPost)
                    );
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request", ex);
            }
        }

        private async Task<bool> ValidateCategories(IEnumerable<Guid> categoryIds)
        {
            return await _categoryRepository.ValidateCategories(categoryIds);
        }
    }
}
