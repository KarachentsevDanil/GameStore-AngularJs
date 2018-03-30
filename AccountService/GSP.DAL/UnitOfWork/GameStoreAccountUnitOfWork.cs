using GSP.Accounts.DAL.Context;
using GSP.Accounts.DAL.Repositories.Contracts;
using GSP.Accounts.DAL.UnitOfWork.Contracts;
using GSP.Common.DAL.UnitOfWork;

namespace GSP.Accounts.DAL.UnitOfWork
{
    public class GameStoreAccountUnitOfWork : GameStoreUnitOfWork<GameStoreAccountContext>, IGameStoreAccountUnitOfWork
    {

        public GameStoreAccountUnitOfWork(GameStoreAccountContext context) : base(context)
        { }

        public ICustomerRepository CustomerRepository { get; set; }
    }
}