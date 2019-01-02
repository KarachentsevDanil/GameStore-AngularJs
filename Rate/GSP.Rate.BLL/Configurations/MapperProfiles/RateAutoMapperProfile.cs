using AutoMapper;
using GSP.Domain.Games;
using GSP.Rate.BLL.DTOs.Rate;

namespace GSP.Rate.BLL.Configurations.MapperProfiles
{
    public class RateAutoMapperProfile : Profile
    {
        public RateAutoMapperProfile()
        {
            CreateMap<RateBase, RateDto>()
                .ForMember(x => x.CreatedOnDate, p => p.MapFrom(t => t.CreatedOn))
                .ForMember(x => x.CreatedOn, p => p.MapFrom(t => t.CreatedOn.ToShortDateString()));

            CreateMap<RateDto, RateBase>();

            CreateMap<CreateRateDto, RateBase>();
        }
    }
}