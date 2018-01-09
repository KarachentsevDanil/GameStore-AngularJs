using AutoMapper;
using GSP.Domain.Games;
using GSP.WebClient.ViewModels;

namespace GSP.WebClient.Infrastracture.Mapper
{
    public class GameAutoMapperProfile : Profile
    {
        public GameAutoMapperProfile()
        {
            CreateMap<Game, GameViewModel>()
                .ForMember(x => x.PhotoData, p => p.Ignore())
                .ForMember(x => x.CategoryName, p => p.MapFrom(t => t.Category.Name));
        }
    }
}
