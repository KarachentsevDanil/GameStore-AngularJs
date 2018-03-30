using System.Collections.Generic;
using System.Linq;
using GSP.DAL.Context;
using GSP.DAL.Repositories.Contracts;
using GSP.Domain.Customers;

namespace GSP.DAL.Repositories
{
    public class CustomerRepository: GameStoreRepository<Customer>, ICustomerRepository
    {
        private readonly GameStoreContext _dbContext;

        public CustomerRepository(GameStoreContext dbContext) : base(dbContext)
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
