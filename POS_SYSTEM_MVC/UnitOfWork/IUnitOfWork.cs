using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;
using POS_SYSTEM_MVC.Repositories.Brands;
using POS_SYSTEM_MVC.Repositories.CategoryRepo;
using POS_SYSTEM_MVC.Repositories.SubCategories;
using POS_SYSTEM_MVC.Repositories.UnitRepo;
using POS_SYSTEM_MVC.Repositories.ProductRepo;
using POS_SYSTEM_MVC.Repositories.SalesRepo;

namespace POS_SYSTEM_MVC.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Categories { get; }
        ISubCategoryRepository SubCategories { get; }
        IBrandRepository Brands { get; }
        IUnitRepository Units { get; }
        IProductRepository Products { get; }
        ISaleRepository Sales { get; }
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
