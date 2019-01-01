using GSP.Account.DAL.Repositories.Contracts;
using GSP.DAL.UnitOfWork.Contracts;

namespace GSP.Account.DAL.UnitOfWork.Contracts
{
    public interface ICustomerUnitOfWork : IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; set; }
    }
}