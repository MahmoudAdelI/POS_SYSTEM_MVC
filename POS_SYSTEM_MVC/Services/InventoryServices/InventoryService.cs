using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.DTOs.Inventory;


namespace POS_SYSTEM_MVC.Services.InventoryServices
{
    public class InventoryService : IInventoryService
    {
        private readonly POSContext _context;
        private const int LowStockThreshold = 10;

        public InventoryService(POSContext context)
        {
            _context = context;
        }

        public async Task<InventoryDto> GetInventoryDataAsync(string? search = null)
        {
            var query = _context.Products
                .Include(p => p.Brand)
                .Include(p => p.SubCategory)
                    .ThenInclude(sc => sc.Category)
                .Include(p => p.Variants)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(p =>
                    p.Name.Contains(search) ||
                    p.Brand.Name.Contains(search) ||
                    p.SubCategory.Category.Name.Contains(search));

            var products = await query.ToListAsync();

            var productDtos = products.Select(p => new InventoryProductDto
            {
                ProductId = p.Id,
                ProductName = p.Name,
                BrandName = p.Brand?.Name ?? "",
                CategoryName = p.SubCategory?.Category?.Name ?? "",
                TotalStock = p.Variants.Sum(v => v.StockQuantity),
                VariantsCount = p.Variants.Count,
                OutOfStockVariants = p.Variants.Count(v => v.StockQuantity == 0),
                LowStockVariants = p.Variants.Count(v => v.StockQuantity > 0 && v.StockQuantity <= LowStockThreshold),
            }).ToList();

            return new InventoryDto
            {
                TotalProducts = productDtos.Count,
                LowStockItems = productDtos.Count(p => p.LowStockVariants > 0 && p.OutOfStockVariants == 0),
                OutOfStockItems = productDtos.Count(p => p.OutOfStockVariants > 0),
                Products = productDtos
            };
        }
    }
}


