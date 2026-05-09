using POS_SYSTEM_MVC.DTOs.Product;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.ProductServices;

public interface IProductService
{
    Task<(IReadOnlyList<Product> Products, int TotalItems)> GetProductsForCashierAsync(string searchTerm, int? categoryId, int? subCategoryId, string stockFilter, int page, int pageSize);
    Task<Product?> GetProductDetailsAsync(int id);
    Task AddProductWithVariants(AddProductDto product);
}