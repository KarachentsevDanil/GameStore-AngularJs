using AutoMapper;
using GSP.Account.BLL.DTOs.Customer;
using GSP.Account.BLL.Services.Contracts;
using GSP.Account.DAL.UnitOfWork.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GSP.Account.BLL.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<CustomerService> _logger;

        public CustomerService(
            ICustomerUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<CustomerService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<CustomerDto> GetCustomerByTermAsync(string term, CancellationToken ct = default)
        {
            _logger.LogInformation("Get customer by term={term}", term);

            var customer = await _unitOfWork.CustomerRepository.GetCustomerByTermAsync(term, ct);
            var customerDto = _mapper.Map<CustomerDto>(customer);

            return customerDto;
        }

        public async Task<IEnumerable<CustomerDto>> GetCustomersAsync(CancellationToken ct = default)
        {
            _logger.LogInformation("Get customer list");

            var customers = await _unitOfWork.CustomerRepository.GetCustomersAsync(ct);
            var customerDtoList = _mapper.Map<List<CustomerDto>>(customers);

            return customerDtoList;
        }
    }
}