using GSP.Order.DAL.Repositories.Contracts;

namespace GSP.DAL.UnitOfWork.Contracts
{
    public interface IOrderUnitOfWork : IUnitOfWork
    {
        IOrderRepository OrderRepository { get; set; }
    }
}