using AutoMapper;
using GSP.Orders.BLL.Dto.Payment;
using GSP.Orders.Domain.Payments;

namespace GSP.Orders.BLL.Mapper
{
    public class PaymentAutoMapperProfile : Profile
    {
        public PaymentAutoMapperProfile()
        {
            CreateMap<Payment, PaymentDto>();

            CreateMap<AddPaymentDto, Payment>()
                .ForMember(x => x.PaymentId, p => p.Ignore())
                .ForMember(x => x.Orders, p => p.Ignore());
        }
    }
}
