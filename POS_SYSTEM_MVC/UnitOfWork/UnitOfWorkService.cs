
using Microsoft.EntityFrameworkCore.Storage;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories;
using POS_SYSTEM_MVC.Repositories.Base;
using POS_SYSTEM_MVC.Repositories.Brands;
using POS_SYSTEM_MVC.Repositories.CategoryRepo;
using POS_SYSTEM_MVC.Repositories.DiscountRepo;
using POS_SYSTEM_MVC.Repositories.ProductRepo;
using POS_SYSTEM_MVC.Repositories.SalesRepo;
using POS_SYSTEM_MVC.Repositories.SubCategories;
using POS_SYSTEM_MVC.Repositories.UnitRepo;

namespace POS_SYSTEM_MVC.UnitOfWork
{
    public class UnitOfWorkService(POSContext context) : IUnitOfWork
    {
        private readonly POSContext _context = context;
        private IDbContextTransaction? _transaction;

        public ICategoryRepository Categories { get; } = new CategoryRepository(context);
        public ISubCategoryRepository SubCategories { get; } = new SubCategoryRepository(context);
        public IBrandRepository Brands { get; } = new BrandRepository(context);
        public IUnitRepository Units { get; } = new UnitRepository(context);
        public IProductRepository Products { get; } = new ProductRepository(context);
        public ISaleRepository Sales { get; } = new SaleRepository(context);
        public IDiscountRepository Discounts { get; }= new DiscountRepository(context);

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
