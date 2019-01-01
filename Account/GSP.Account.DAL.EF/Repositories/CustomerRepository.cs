using GSP.Account.DAL.EF.Context;
using GSP.Account.DAL.Repositories.Contracts;
using GSP.Account.Domain.Customers;
using GSP.DAL.EF.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Account.DAL.EF.Repositories
{
    public class CustomerRepository : BaseRepository<string, Customer, CustomerContext>, ICustomerRepository
    {
        private readonly CustomerContext _dbContext;

        public CustomerRepository(CustomerContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Customer> GetCustomerByTermAsync(string term, CancellationToken ct = default)
        {
            var customer = await _dbContext.Customers.FirstOrDefaultAsync(x => x.Email.ToLower() == term.ToLower(), ct);
            return customer;
        }

        public async Task<IEnumerable<Customer>> GetCustomersAsync(CancellationToken ct = default)
        {
            return await _dbContext.Customers.ToListAsync(ct);
        }
    }
}