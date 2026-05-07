using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.ProductRepo;

public interface IProductRepository : IBaseRepository<Product>
{
    Task<(IReadOnlyList<Product> Products, int TotalItems)> GetProductsForCashierAsync(string searchTerm, int? categoryId, int? subCategoryId, int page, int pageSize);
    Task<Product?> GetProductDetailsAsync(int id);
}