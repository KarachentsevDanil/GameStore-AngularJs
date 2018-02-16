using AutoMapper;
using GSP.BLL.Dto.Customer;
using GSP.Domain.Customers;

namespace GSP.BLL.Mapper
{
    public class CustomerAutoMapperProfile:Profile
    {
        public CustomerAutoMapperProfile()
        {
            CreateMap<Customer, CustomerDto>();
        }
    }
}
