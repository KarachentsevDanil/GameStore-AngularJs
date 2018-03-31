using AutoMapper;
using GSP.Games.BLL.Dto.Category;
using GSP.Games.Domain.Games;

namespace GSP.Games.BLL.Mapper
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