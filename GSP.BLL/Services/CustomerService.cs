using System.Collections.Generic;
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

        public void AddCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Add(customer);
            _unitOfWork.Commit();
        }

        public void UpdateCustomer(Customer customer)
        {
            _unitOfWork.CustomerRepository.Update(customer);
            _unitOfWork.Commit();
        }

        public Customer GetCustomerByTerm(string term)
        {
            return _unitOfWork.CustomerRepository.GetCustomerByTerm(term);
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _unitOfWork.CustomerRepository.GetCustomers();
        }
    }
}
