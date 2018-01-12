using AutoMapper;
using GSP.Domain.Orders;
using GSP.WebClient.ViewModels;

namespace GSP.WebClient.Infrastracture.Mapper
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<Order, OrderViewModel>()
                .ForMember(x => x.Games, p => p.Ignore())
                .ForMember(x => x.SaleDate, p => p.MapFrom(t => $"{t.SaleDate.ToShortDateString()} {t.SaleDate.ToShortTimeString()}"))
                .ForMember(x => x.Customer, p => p.MapFrom(t => t.Customer.FullName));
        }
    }
}
