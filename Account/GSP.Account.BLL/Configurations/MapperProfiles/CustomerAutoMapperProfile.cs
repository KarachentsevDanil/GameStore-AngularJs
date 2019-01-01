using AutoMapper;
using GSP.Account.BLL.DTOs.Customer;
using GSP.Account.Domain.Customers;

namespace GSP.Account.BLL.Configurations.MapperProfiles
{
    public class CustomerAutoMapperProfile : Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerDto>()
                .ForMember(x => x.CustomerId, t => t.MapFrom(p => p.Id))
                .ForMember(x => x.Role, t => t.MapFrom(p => p.Role.ToString()));
        }
    }
}