using System.Collections.Generic;
using GSP.BLL.Dto.Payment;
using GSP.BLL.Services.Contracts;
using GSP.DAL.UnitOfWork.Contracts;
using GSP.Domain.Payments;

namespace GSP.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IGameStoreUnitOfWork _unitOfWork;

        public PaymentService(IGameStoreUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public int AddPayment(AddPaymentDto payment)
        {
            var addPayment = AutoMapper.Mapper.Map<AddPaymentDto, Payment>(payment);

            _unitOfWork.PaymentRepository.Add(addPayment);
            _unitOfWork.Commit();

            return addPayment.PaymentId;
        }

        public IEnumerable<PaymentDto> GetCustomerPayments(string customerId)
        {
            var payments = _unitOfWork.PaymentRepository.GetCustomerPayments(customerId);
            return AutoMapper.Mapper.Map<IEnumerable<Payment>, List<PaymentDto>>(payments);
        }
    }
}
