using AutoMapper;
using GSP.Payment.BLL.DTOs.Payment;
using GSP.Payment.Domain.Payments;

namespace GSP.Payment.BLL.Configurations.MapperProfiles
{
    public class PaymentAutoMapperProfile : Profile
    {
        public PaymentAutoMapperProfile()
        {
            CreateMap<PaymentBase, PaymentDto>();

            CreateMap<AddPaymentDto, PaymentBase>()
                .ForMember(x => x.PaymentId, p => p.Ignore());
        }
    }
}