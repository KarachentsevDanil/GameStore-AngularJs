using AutoMapper;
using GSP.BLL.Dto.Rate;
using GSP.Domain.Games;

namespace GSP.BLL.Mapper
{
    public class RateAutoMapperConfig: Profile
    {
        public RateAutoMapperConfig()
        {
            CreateMap<Rate, RateDto>()
                .ForMember(x => x.CustomerName, p => p.MapFrom(t => t.Customer.FullName))
                .ForMember(x => x.GameName, p => p.MapFrom(t => t.Game.Name))
                .ForMember(x => x.CreatedOn, p => p.MapFrom(t => t.CreatedOn.ToShortDateString()));

            CreateMap<CreateRateDto, Rate>()
                .ForMember(x => x.Game, p => p.Ignore())
                .ForMember(x => x.Customer, p => p.Ignore());
        }
    }
}
