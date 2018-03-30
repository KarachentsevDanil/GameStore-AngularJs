using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using GSP.BLL.Dto.Game;
using GSP.BLL.Dto.Order;
using GSP.Domain.Games;
using GSP.Domain.Orders;

namespace GSP.BLL.Mapper
{
    public class OrderAutoMapperProfile : Profile
    {
        public OrderAutoMapperProfile()
        {
            CreateMap<Order, OrderDto>()
                .ForMember(x => x.Games, p => p.MapFrom(t => AutoMapper.Mapper.Map<ICollection<Game>, List<GameDto>>(t.Games.Select(g => g.Game).ToList())))
                .ForMember(x => x.SaleDate, p => p.MapFrom(t => $"{t.SaleDate.ToShortDateString()} {t.SaleDate.ToShortTimeString()}"))
                .ForMember(x => x.CustomerName, p => p.MapFrom(t => t.Customer.FullName));

            CreateMap<CreateOrderDto, Order>()
                .ForMember(x => x.SaleDate, p => p.Ignore())
                .ForMember(x => x.IsDeleted, p => p.Ignore())
                .ForMember(x => x.Status, p => p.Ignore())
                .ForMember(x => x.Customer, p => p.Ignore())
                .ForMember(x => x.Games, p => p.Ignore())
                .ForMember(x => x.Payment, p => p.Ignore())
                .ForMember(x => x.PaymentId, p => p.Ignore());
        }
    }
}