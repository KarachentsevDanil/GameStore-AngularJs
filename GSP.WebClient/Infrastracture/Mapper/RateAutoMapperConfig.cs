using AutoMapper;
using GSP.Domain.Games;
using GSP.WebClient.ViewModels;

namespace GSP.WebClient.Infrastracture.Mapper
{
    public class RateAutoMapperConfig: Profile
    {
        public RateAutoMapperConfig()
        {
            CreateMap<Rate, RateViewModel>()
                .ForMember(x => x.Customer, p => p.MapFrom(t => t.Customer.FullName))
                .ForMember(x => x.CreatedOn, p => p.MapFrom(t => t.CreatedOn.ToShortDateString()));
        }
    }
}
