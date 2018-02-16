using System.Collections.Generic;
using GSP.BLL.Dto.Customer;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Customers;

namespace GSP.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;

        public CustomerService(IGameStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public CustomerDto GetCustomerByTerm(string term)
        {
            var customer = _unitOfWork.CustomerRepository.GetCustomerByTerm(term);
            return AutoMapper.Mapper.Map<Customer, CustomerDto>(customer);
        }

        public IEnumerable<CustomerDto> GetCustomers()
        {
            var customers = _unitOfWork.CustomerRepository.GetCustomers();
            return AutoMapper.Mapper.Map<IEnumerable<Customer>, List<CustomerDto>>(customers);
        }
    }
}
