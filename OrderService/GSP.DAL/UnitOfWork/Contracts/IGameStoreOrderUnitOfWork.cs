using GSP.Common.DAL.UnitOfWork.Contracts;
using GSP.Orders.DAL.Repositories.Contracts;

namespace GSP.Orders.DAL.UnitOfWork.Contracts
{
    public interface IGameStoreOrderUnitOfWork : IGameStoreUnitOfWork
    {
        IOrderRepository OrderRepository{ get; set; }

        IPaymentRepository PaymentRepository { get; set; }
    }
}
