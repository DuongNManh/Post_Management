using AutoMapper;
using Post_Management.API.Models.Domains;
using Post_Management.API.Models.DTOs;
using Post_Management.API.Models.Responses;
using Post_Management.API.Repositories;

namespace Post_Management.API.Converter
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryDTO, Category>()
               .ForMember(dest => dest.Id, opt => opt.Ignore());

            CreateMap<BlogPostDTO, BlogPost>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Categories, opt => opt.Ignore());

            CreateMap<BlogPost, BlogPostResponse>()
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories));
            CreateMap<Category, CategoryResponse>();
        }
    }
}
