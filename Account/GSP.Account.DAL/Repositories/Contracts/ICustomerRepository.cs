using GSP.Account.Domain.Customers;
using GSP.DAL.Repositories.Contracts;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Account.DAL.Repositories.Contracts
{
    public interface ICustomerRepository : IGenericRepository<string, Customer>
    {
        Task<Customer> GetCustomerByTermAsync(string term, CancellationToken ct = default);

        Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken ct = default);
    }
}