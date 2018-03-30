using GSP.DAL.Repositories.Contracts;

namespace GSP.DAL.UnitOfWork.Contracts
{
    public interface IGameStoreUnitOfWork
    {
        IGameRepository GameRepository { get; set; }

        IOrderRepository OrderRepository{ get; set; }

        ICustomerRepository CustomerRepository { get; set; }

        IRateRepository RateRepository { get; set; }

        ICategoryRepository CategoryRepository { get; set; }

        IPaymentRepository PaymentRepository { get; set; }

        void Commit();
    }
}
