using System.Collections.Generic;
using GSP.Domain.Customers;

namespace GSP.DAL.Repositories.Contracts
{
    public interface ICustomerRepository : IGameStoreRepository<Customer>
    {
        Customer GetCustomerByTerm(string term);

        IEnumerable<Customer> GetCustomers();
    }
}
