using System.Collections.Generic;
using GSP.Accounts.BLL.Dto.Customer;
using GSP.Accounts.BLL.Services.Contracts;
using GSP.Accounts.DAL.UnitOfWork.Contracts;
using GSP.Accounts.Domain.Customers;

namespace GSP.Accounts.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGameStoreAccountUnitOfWork _unitOfWork;

        public CustomerService(IGameStoreAccountUnitOfWork unitOfWork)
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
