using AutoMapper;
using GSP.Order.BLL.DTOs.Order;
using GSP.Order.Domain.Orders;

namespace GSP.Order.BLL.Configurations.MapperProfiles
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<OrderBase, OrderDto>()
                .ForMember(x => x.Games, p => p.Ignore())
                .ForMember(x => x.SaleDate, p => p.MapFrom(t => $"{t.SaleDate.ToShortDateString()} {t.SaleDate.ToShortTimeString()}"));

            CreateMap<CreateOrderDto, OrderBase>()
                .ForMember(x => x.SaleDate, p => p.Ignore())
                .ForMember(x => x.IsDeleted, p => p.Ignore())
                .ForMember(x => x.Status, p => p.Ignore())
                .ForMember(x => x.Games, p => p.Ignore())
                .ForMember(x => x.PaymentId, p => p.Ignore());
        }
    }
}