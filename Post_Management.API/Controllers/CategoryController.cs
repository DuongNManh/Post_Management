using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post_Management.API.Data;
using Post_Management.API.Models.DTOs;
using Post_Management.API.Models.Domains;
using Post_Management.API.Repositories;
using Post_Management.API.Models;
using System.Globalization;
using Azure;
using Post_Management.API.CustomActionFilters;
using Post_Management.API.Exceptions;
using Post_Management.API.Models.Responses;

namespace Post_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        //Get: 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var paginatedResult = await _categoryRepository.GetAllCategories();
            var categoryResponse = paginatedResult.Select(category => _mapper.Map<CategoryResponse>(category));

            var response = ApiResponseBuilder.BuildResponse(
                statusCode: StatusCodes.Status200OK,
                message: "Success",
                data: categoryResponse
                );

            return Ok(response);

        }

        //Post: /api/Category
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CategoryDTO valueDTO)
        {
            var createdCategory = await _categoryRepository.CreateCategory(_mapper.Map<Category>(valueDTO));

            var response = ApiResponseBuilder.BuildResponse(
                statusCode: 201, // Created status code
                data: createdCategory,
                message: "Category created successfully");

            return CreatedAtAction(nameof(Get),
                new { id = createdCategory.Id },
                response);
        }

        [HttpGet]
        [Route("{id:guid}")]
        [ValidateModelAttributes]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var category = await _categoryRepository.GetCategoryById(id);
                if (category == null)
                {
                    throw new NotFoundException("Category not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    data: _mapper.Map<CategoryResponse>(category),
                    message: "Category retrieved successfully");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpPut]
        [Route("{id:guid}")]
        [ValidateModelAttributes]
        public async Task<IActionResult> Put(Guid id, CategoryDTO categoryDTO)
        {
            try
            {
                var category = await _categoryRepository.UpdateCategory(id, _mapper.Map<Category>(categoryDTO));
                if (category == null)
                {
                    throw new NotFoundException("Category not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    data: _mapper.Map<CategoryResponse>(category),
                    message: "Category updated successfully");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                var category = await _categoryRepository.DeleteCategory(id);
                if (category == null)
                {
                    throw new NotFoundException("Category not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    data: _mapper.Map<CategoryResponse>(category),
                    message: "Category deleted successfully");
                return Ok(response);
            }
            catch (NotFoundException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
