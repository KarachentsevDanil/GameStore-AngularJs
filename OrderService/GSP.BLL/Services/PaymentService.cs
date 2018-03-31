using System.Collections.Generic;
using GSP.Orders.BLL.Dto.Payment;
using GSP.Orders.BLL.Services.Contracts;
using GSP.Orders.DAL.UnitOfWork.Contracts;
using GSP.Orders.Domain.Payments;

namespace GSP.Orders.BLL.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IGameStoreOrderUnitOfWork _unitOfWork;

        public PaymentService(IGameStoreOrderUnitOfWork unitOfWork)
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
