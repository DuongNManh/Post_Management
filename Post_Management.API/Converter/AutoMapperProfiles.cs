using AutoMapper;
using Post_Management.API.Models.Domains;
using Post_Management.API.Models.DTOs;

namespace Post_Management.API.Converter
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<CategoryDTO, Category>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<BlogPostDTO, BlogPost>()
                .ForMember(dest => dest.Id, opt => opt.Ignore());
            CreateMap<Category, CategoryDTO>();
            CreateMap<BlogPost, BlogPostDTO>();
        }
    }
}
