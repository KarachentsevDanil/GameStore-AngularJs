using System.Collections.Generic;
using GSP.Accounts.Domain.Customers;
using GSP.Common.DAL.Repositories.Contracts;

namespace GSP.Accounts.DAL.Repositories.Contracts
{
    public interface ICustomerRepository : IGameStoreRepository<Customer>
    {
        Customer GetCustomerByTerm(string term);

        IEnumerable<Customer> GetCustomers();
    }
}
