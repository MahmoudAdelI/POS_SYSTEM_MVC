using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.ProductServices;

public class ProductService : IProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<(IReadOnlyList<Product> Products, int TotalItems)> GetProductsForCashierAsync(string searchTerm, int? categoryId, int? subCategoryId, string stockFilter, int page, int pageSize)
    {
        return await _unitOfWork.Products.GetProductsForCashierAsync(searchTerm, categoryId, subCategoryId, stockFilter, page, pageSize);
    }

    public async Task<Product?> GetProductDetailsAsync(int id)
    {
        return await _unitOfWork.Products.GetProductDetailsAsync(id);
    }
}