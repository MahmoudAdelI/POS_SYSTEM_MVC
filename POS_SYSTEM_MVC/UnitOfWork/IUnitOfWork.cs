using POS_SYSTEM_MVC.Repositories;
using POS_SYSTEM_MVC.Repositories.UnitRepo;

namespace POS_SYSTEM_MVC.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IUnitRepository Units { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
