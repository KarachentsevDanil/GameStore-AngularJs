using GSP.Account.BLL.DTOs.Customer;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Account.BLL.Services.Contracts
{
    public interface ICustomerService
    {
        Task<CustomerDto> GetCustomerByTermAsync(string term, CancellationToken ct = default);

        Task<IEnumerable<CustomerDto>> GetCustomersAsync(CancellationToken ct = default);
    }
}