using GSP.Game.DAL.Repositories.Contracts;

namespace GSP.DAL.UnitOfWork.Contracts
{
    public interface IGameUnitOfWork : IUnitOfWork
    {
        IGameRepository GameRepository { get; set; }

        ICategoryRepository CategoryRepository { get; set; }
    }
}