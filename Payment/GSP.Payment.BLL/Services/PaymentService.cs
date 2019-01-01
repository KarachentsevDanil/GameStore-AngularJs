using AutoMapper;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Payment.BLL.DTOs.Payment;
using GSP.Payment.BLL.Services.Contracts;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using GSP.Payment.DAL.UnitOfWork.Contracts;
using GSP.Payment.Domain.Payments;

namespace GSP.Payment.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentUnitOfWork _unitOfWork;

        private readonly IMapper _mapper;

        private readonly ILogger<PaymentService> _logger;

        public PaymentService(
            IPaymentUnitOfWork unitOfWork,
            IMapper mapper,
            ILogger<PaymentService> logger)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> AddPaymentAsync(AddPaymentDto payment, CancellationToken ct = default)
        {
            _logger.LogInformation("Add new payment {payment}", payment);

            var addPayment = _mapper.Map<PaymentBase>(payment);

            _unitOfWork.PaymentRepository.Create(addPayment);
            await _unitOfWork.CommitAsync(ct);

            return addPayment.PaymentId;
        }

        public async Task<IEnumerable<PaymentDto>> GetCustomerPaymentsAsync(string customerId, CancellationToken ct = default)
        {
            _logger.LogInformation("Get customer payments");

            var payments = await _unitOfWork.PaymentRepository.GetCustomerPaymentsAsync(customerId, ct);
            var paymentListDto = _mapper.Map<List<PaymentDto>>(payments);

            return paymentListDto;
        }
    }
}