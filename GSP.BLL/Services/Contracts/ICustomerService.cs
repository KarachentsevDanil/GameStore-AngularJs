using System.Collections.Generic;
using GSP.Domain.Customers;

namespace GSP.BLL.Services.Contracts
{
    public interface ICustomerService
    {
        void AddCustomer(Customer customer);

        void UpdateCustomer(Customer customer);

        Customer GetCustomerByTerm(string term);

        IEnumerable<Customer> GetCustomers();
    }
}
