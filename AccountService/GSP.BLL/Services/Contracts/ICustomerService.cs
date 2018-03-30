using System.Collections.Generic;
using GSP.Accounts.BLL.Dto.Customer;

namespace GSP.Accounts.BLL.Services.Contracts
{
    public interface ICustomerService
    {
        CustomerDto GetCustomerByTerm(string term);

        IEnumerable<CustomerDto> GetCustomers();
    }
}