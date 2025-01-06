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
    public class CategoryController(ICategoryRepository categoryRepository, IMapper mapper) : ControllerBase
    {

        //Get: 
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {

            var paginatedResult = await categoryRepository.GetAllCategories();
            var categoryResponse = paginatedResult.Select(category => mapper.Map<CategoryResponse>(category));

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
            var createdCategory = await categoryRepository.CreateCategory(mapper.Map<Category>(valueDTO));

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
                var category = await categoryRepository.GetCategoryById(id);
                if (category == null)
                {
                    throw new NotFoundException("Category not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    data: mapper.Map<CategoryResponse>(category),
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
                var category = await categoryRepository.UpdateCategory(id, mapper.Map<Category>(categoryDTO));
                if (category == null)
                {
                    throw new NotFoundException("Category not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    data: mapper.Map<CategoryResponse>(category),
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
                var category = await categoryRepository.DeleteCategory(id);
                if (category == null)
                {
                    throw new NotFoundException("Category not found");
                }
                var response = ApiResponseBuilder.BuildResponse(
                    statusCode: 200,
                    data: mapper.Map<CategoryResponse>(category),
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
