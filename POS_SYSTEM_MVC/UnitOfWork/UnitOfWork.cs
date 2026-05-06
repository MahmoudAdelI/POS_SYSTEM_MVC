
using Microsoft.EntityFrameworkCore.Storage;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Repositories;

namespace POS_SYSTEM_MVC.UnitOfWork
{
    public class UnitOfWork(POSContext context) : IUnitOfWork
    {
        private readonly POSContext _context = context;
        private IDbContextTransaction? _transaction;

        public IUnitRepository Units { get; } = new UnitRepository(context);

        public async Task BeginTransactionAsync()
        {
            _transaction = await _context.Database.BeginTransactionAsync();
        }

        public async Task CommitTransactionAsync()
        {
            if (_transaction is null) throw new InvalidOperationException("No transaction in progress.");
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task RollbackTransactionAsync()
        {
            if (_transaction is null) return;
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }

        public async Task<int> SaveChangesAsync() => await _context.SaveChangesAsync();
            
        public void Dispose()
        {
            _transaction?.Dispose();
            _context.Dispose();
        }
    }
}
