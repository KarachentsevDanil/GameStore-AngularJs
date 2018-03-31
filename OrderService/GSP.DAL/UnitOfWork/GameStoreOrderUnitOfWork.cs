using GSP.Common.DAL.UnitOfWork;
using GSP.Orders.DAL.Context;
using GSP.Orders.DAL.Repositories.Contracts;
using GSP.Orders.DAL.UnitOfWork.Contracts;

namespace GSP.Orders.DAL.UnitOfWork
{
    public class GameStoreOrderUnitOfWork : GameStoreUnitOfWork<GameStoreOrderContext>, IGameStoreOrderUnitOfWork
    {
        public GameStoreOrderUnitOfWork(GameStoreOrderContext context) : base(context)
        { }
        
        public IOrderRepository OrderRepository { get; set; }

        public IPaymentRepository PaymentRepository { get; set; }
    }
}
