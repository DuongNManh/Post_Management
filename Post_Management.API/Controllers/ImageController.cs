using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Post_Management.API.CustomActionFilters;
using Post_Management.API.Data.Models;
using Post_Management.API.Data.Models.Domains;
using Post_Management.API.Data.Models.DTOs;
using Post_Management.API.Data.Models.Responses;
using Post_Management.API.Repositories;
using System.Net;

namespace Post_Management.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController(IImageRepository imageRepository, IMapper mapper) : ControllerBase
    {
        //POST: api/Images
        [HttpPost]
        [ValidateModelAttributes]
        [ValidateFile(maxSizeInMB: 5, allowedExtensions: [".png", ".jpg", ".jpeg"])]
        [Consumes("multipart/form-data")] // Add this attribute
        [ProducesResponseType(typeof(ApiResponse<BlogImageResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UploadImage([FromForm] BlogImageDTO blogImageDTO)
        {
            var blogImage = mapper.Map<BlogImage>(blogImageDTO);

            blogImage = await imageRepository.AddImageAsync(blogImageDTO.File, blogImage);
            return Ok(ApiResponseBuilder.BuildResponse<BlogImageResponse>(
                statusCode: (int)HttpStatusCode.Created,
                message: "Add image successfully",
                data: mapper.Map<BlogImageResponse>(blogImage))
            );
        }

        [HttpGet]
        public async Task<IActionResult> GetAllImages()
        {
            // call the repository to get all images
            var images = await imageRepository.GetAllImagesAsync();
            return Ok(ApiResponseBuilder.BuildResponse<IEnumerable<BlogImageResponse>>(
                statusCode: (int)HttpStatusCode.OK,
                message: "Get all images successfully",
                data: mapper.Map<IEnumerable<BlogImageResponse>>(images))
            );

        }
    }
}
