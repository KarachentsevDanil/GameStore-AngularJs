using GSP.DAL.UnitOfWork.Contracts;
using GSP.Payment.DAL.Repositories.Contracts;

namespace GSP.Payment.DAL.UnitOfWork.Contracts
{
    public interface IPaymentUnitOfWork : IUnitOfWork
    {
        IPaymentRepository PaymentRepository { get; set; }
    }
}