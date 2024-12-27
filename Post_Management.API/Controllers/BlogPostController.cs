using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post_Management.API.CustomActionFilters;
using Post_Management.API.Exceptions;
using Post_Management.API.Models;
using Post_Management.API.Models.Domains;
using Post_Management.API.Models.DTOs;
using Post_Management.API.Repositories;

namespace Post_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostController : ControllerBase
    {
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IMapper _mapper;

        public BlogPostController(IBlogPostRepository blogPostRepository, IMapper mapper)
        {
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
        }

        //Get:
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var paginatedResult = await _blogPostRepository.GetAllBlogPosts();
            var response = ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                message: "Success",
                data: paginatedResult
                );
            return Ok(response);
        }

        [HttpGet]
        [Route("{id: guid}")]
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
                    data: blogPost
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
                var createdBlogPost = await _blogPostRepository.CreateBlogPost(_mapper.Map<BlogPost>(valueDTO));
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: StatusCodes.Status201Created,
                    message: "Blog post created successfully",
                    data: createdBlogPost
                    );
                return CreatedAtAction(nameof(Get),
                    new { id = createdBlogPost.Id },
                    response);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while processing your request", ex);
            }
        }

        //Put: /api/BlogPost/{id}
        [HttpPut]
        [Route("{id: guid}")]
        [ValidateModelAttributes]
        public async Task<IActionResult> Put(Guid id, BlogPostDTO blogPostDTO)
        {
            try
            {
                var updatedBlogPost = await _blogPostRepository.UpdateBlogPost(id, _mapper.Map<BlogPost>(blogPostDTO));
                if (updatedBlogPost == null)
                {
                    throw new NotFoundException("Blog post not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: StatusCodes.Status200OK,
                    message: "Blog post updated successfully",
                    data: updatedBlogPost
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
        [Route("{id: guid}")]
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
                    data: deletedBlogPost
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
    }
}
