using AutoMapper;
using GSP.BLL.Dto.Payment;
using GSP.Domain.Payments;

namespace GSP.BLL.Mapper
{
    public class PaymentAutoMapperProfile : Profile
    {
        public PaymentAutoMapperProfile()
        {
            CreateMap<Payment, PaymentDto>()
                .ForMember(x => x.CustomerName, p => p.MapFrom(c => c.Customer.FullName));

            CreateMap<AddPaymentDto, Payment>()
                .ForMember(x => x.PaymentId, p => p.Ignore())
                .ForMember(x => x.Customer, p => p.Ignore())
                .ForMember(x => x.Orders, p => p.Ignore());
        }
    }
}
