using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;
using POS_SYSTEM_MVC.Repositories.Brands;
using POS_SYSTEM_MVC.Repositories.CategoryRepo;
using POS_SYSTEM_MVC.Repositories.DiscountRepo;
using POS_SYSTEM_MVC.Repositories.ProductRepo;
using POS_SYSTEM_MVC.Repositories.SalesRepo;
<<<<<<< HEAD
using POS_SYSTEM_MVC.Repositories.ProductVariantRepo;
=======
using POS_SYSTEM_MVC.Repositories.SubCategories;
using POS_SYSTEM_MVC.Repositories.UnitRepo;
>>>>>>> Discount_MOW

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
<<<<<<< HEAD
        IProductVariantRepository ProductVariants { get; }
        IBaseRepository<ProductAttribute> Attributes { get; }
        IBaseRepository<ProductAttributeValue> AttributeValues { get; }
=======
        IDiscountRepository Discounts { get; }
>>>>>>> Discount_MOW
        Task<int> SaveChangesAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}
