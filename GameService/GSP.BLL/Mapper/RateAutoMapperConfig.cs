using AutoMapper;
using GSP.Games.BLL.Dto.Rate;
using GSP.Games.Domain.Games;

namespace GSP.Games.BLL.Mapper
{
    public class RateAutoMapperConfig: Profile
    {
        public RateAutoMapperConfig()
        {
            CreateMap<Rate, RateDto>()
                .ForMember(x => x.CreatedOnDate, p => p.MapFrom(t => t.CreatedOn))
                .ForMember(x => x.GameName, p => p.MapFrom(t => t.Game.Name))
                .ForMember(x => x.CreatedOn, p => p.MapFrom(t => t.CreatedOn.ToShortDateString()));

            CreateMap<RateDto, Rate>()
                .ForMember(x => x.Game, p => p.Ignore());

            CreateMap<CreateRateDto, Rate>()
                .ForMember(x => x.Game, p => p.Ignore());
        }
    }
}
