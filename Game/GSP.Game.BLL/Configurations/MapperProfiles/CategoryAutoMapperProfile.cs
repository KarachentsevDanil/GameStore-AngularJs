using AutoMapper;
using GSP.Game.BLL.DTOs.Category;
using GSP.Game.Domain.Games;

namespace GSP.Game.BLL.Configurations.MapperProfiles
{
    public class CategoryAutoMapperProfile : Profile
    {
        public CategoryAutoMapperProfile()
        {
            CreateMap<Category, CategoryDto>();

            CreateMap<CreateCategoryDto, Category>()
                .ForMember(p => p.CategoryId, t => t.Ignore())
                .ForMember(p => p.Games, t => t.Ignore());
        }
    }
}