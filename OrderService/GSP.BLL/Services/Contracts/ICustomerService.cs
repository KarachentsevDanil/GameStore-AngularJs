using System.Collections.Generic;
using GSP.BLL.Dto.Customer;

namespace GSP.BLL.Services.Contracts
{
    public interface ICustomerService
    {
        CustomerDto GetCustomerByTerm(string term);

        IEnumerable<CustomerDto> GetCustomers();
    }
}