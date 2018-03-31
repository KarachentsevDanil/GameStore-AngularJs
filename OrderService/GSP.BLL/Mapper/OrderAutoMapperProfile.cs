using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GSP.Orders.BLL.Dto.Order;
using GSP.Orders.Domain.Orders;

namespace GSP.Orders.BLL.Mapper
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Games,
                    p => p.MapFrom(t =>
                        AutoMapper.Mapper.Map<ICollection<OrderGame>, List<GameDto>>(t.Games.ToList())))
                .ForMember(x => x.SaleDate,
                    p => p.MapFrom(t => $"{t.SaleDate.ToShortDateString()} {t.SaleDate.ToShortTimeString()}"));

            CreateMap<CreateOrderDto, Order>()
                .ForMember(x => x.SaleDate, p => p.Ignore())
                .ForMember(x => x.IsDeleted, p => p.Ignore())
                .ForMember(x => x.Status, p => p.Ignore())
                .ForMember(x => x.Games, p => p.Ignore())
                .ForMember(x => x.Payment, p => p.Ignore())
                .ForMember(x => x.PaymentId, p => p.Ignore());

            CreateMap<AddGameToBucketDto, OrderGame>()
                .ForMember(x => x.Order, p => p.Ignore());
        }
    }
}