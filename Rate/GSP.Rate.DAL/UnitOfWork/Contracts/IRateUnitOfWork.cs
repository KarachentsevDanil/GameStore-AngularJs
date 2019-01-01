using GSP.DAL.Repositories.Contracts;
using GSP.Rate.DAL.Repositories.Contracts;

namespace GSP.DAL.UnitOfWork.Contracts
{
    public interface IRateUnitOfWork : IUnitOfWork
    {
        IRateRepository RateRepository { get; set; }
    }
}