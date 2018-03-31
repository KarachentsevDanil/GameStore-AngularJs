using GSP.Common.DAL.UnitOfWork.Contracts;
using GSP.Games.DAL.Repositories.Contracts;

namespace GSP.Games.DAL.UnitOfWork.Contracts
{
    public interface IGameStoreGameUnitOfWork : IGameStoreUnitOfWork
    {
        IGameRepository GameRepository { get; set; }

        IRateRepository RateRepository { get; set; }

        ICategoryRepository CategoryRepository { get; set; }
    }
}
