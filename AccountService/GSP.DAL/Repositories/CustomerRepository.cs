using System.Collections.Generic;
using System.Linq;
using GSP.Accounts.DAL.Context;
using GSP.Accounts.DAL.Repositories.Contracts;
using GSP.Accounts.Domain.Customers;
using GSP.Common.DAL.Repositories;

namespace GSP.Accounts.DAL.Repositories
{
    public class CustomerRepository : GameStoreRepository<Customer, GameStoreAccountContext>, ICustomerRepository
    {
        private readonly GameStoreAccountContext _dbContext;

        public CustomerRepository(GameStoreAccountContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public Customer GetCustomerByTerm(string term)
        {
            return _dbContext.Customers.FirstOrDefault(x => x.Email.ToLower() == term.ToLower());
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _dbContext.Customers.ToList();
        }
    }
}
