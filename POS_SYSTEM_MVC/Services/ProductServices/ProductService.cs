using POS_SYSTEM_MVC.DTOs.Product;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.ProductServices;

public class ProductService(IUnitOfWork _unitOfWork) : IProductService
{
    public async Task<(IReadOnlyList<Product> Products, int TotalItems)> GetProductsForCashierAsync(string searchTerm, int? categoryId, int? subCategoryId, string stockFilter, int page, int pageSize)
    {
        return await _unitOfWork.Products.GetProductsForCashierAsync(searchTerm, categoryId, subCategoryId, stockFilter, page, pageSize);
    }

    public async Task<Product?> GetProductDetailsAsync(int id)
    {
        return await _unitOfWork.Products.GetProductDetailsAsync(id);
    }

    public async Task AddProductWithVariants(AddProductDto dto)
    {
        var product = new Product
        {
            Name = dto.Name,
            BrandId = dto.BrandId,
            SubCategoryId = dto.SubcategoryId,
            UnitId = dto.UnitId,
            BasePrice = dto.BasePrice
        };

        await _unitOfWork.Products.AddAsync(product);
        await _unitOfWork.SaveChangesAsync();

        foreach(var v in dto.Variants)
        {
            var variant = new ProductVariant
            {
                ProductId = product.Id,
                UnitPrice = v.Price,
                StockQuantity = v.Stock,
                SKU = GenerateSKU(product.Name),
                VariantAttributes = v.AttributeValues
                .Select(attributeId => new VariantAttribute {AttributeValueId = attributeId})
                .ToList()
            };

            // add product variant
            await _unitOfWork.ProductVariants.AddAsync(variant);
        }

        await _unitOfWork.SaveChangesAsync();
    }

    private string GenerateSKU(string productName)
    {
        string Slugify(string input, int maxLen) =>
        new string(input.ToUpper().Where(char.IsLetterOrDigit).Take(maxLen).ToArray());
        // simple SKU — replace with your own logic
        return $"{Slugify(productName, 4)}-{Guid.NewGuid().ToString()[..6].ToUpper()}";
    }
}