using GSP.Accounts.DAL.Repositories.Contracts;
using GSP.Common.DAL.UnitOfWork.Contracts;

namespace GSP.Accounts.DAL.UnitOfWork.Contracts
{
    public interface IGameStoreAccountUnitOfWork : IGameStoreUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; set; }
    }
}
