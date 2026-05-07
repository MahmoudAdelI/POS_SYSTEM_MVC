using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.ProductRepo;

public class ProductRepository : BaseRepository<Product>, IProductRepository
{
    public ProductRepository(POSContext context) : base(context)
    {
    }

    public async Task<(IReadOnlyList<Product> Products, int TotalItems)> GetProductsForCashierAsync(string searchTerm, int? categoryId, int? subCategoryId, string stockFilter, int page, int pageSize)
    {
        var query = _context.Products
            .AsNoTracking()
            .Include(p => p.Brand)
            .Include(p => p.Variants)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(p => p.Name.Contains(searchTerm) || p.Brand.Name.Contains(searchTerm));
        }

        if (subCategoryId.HasValue)
        {
            query = query.Where(p => p.SubCategoryId == subCategoryId.Value);
        }
        else if (categoryId.HasValue)
        {
            query = query.Where(p => p.SubCategory.CategoryId == categoryId.Value);
        }

        if (!string.IsNullOrEmpty(stockFilter))
        {
            if (stockFilter == "inStock")
            {
                query = query.Where(p => p.Variants.Any(v => v.StockQuantity > 0));
            }
            else if (stockFilter == "outOfStock")
            {
                query = query.Where(p => p.Variants.All(v => v.StockQuantity <= 0));
            }
        }

        var totalItems = await query.CountAsync();
        var products = await query
            .OrderBy(p => p.Name)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return (products, totalItems);
    }

    public async Task<Product?> GetProductDetailsAsync(int id)
    {
        return await _context.Products
            .AsNoTracking()
            .Include(p => p.Brand)
            .Include(p => p.Variants)
                .ThenInclude(v => v.VariantAttributes)
                    .ThenInclude(va => va.AttributeValue)
                        .ThenInclude(av => av.Attribute)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<ProductVariant?> GetVariantByIdAsync(int variantId)
    {
        return await _context.ProductVariants
            .Include(v => v.Product)
            .Include(v => v.VariantAttributes)
                .ThenInclude(va => va.AttributeValue)
            .FirstOrDefaultAsync(v => v.Id == variantId);
    }
}