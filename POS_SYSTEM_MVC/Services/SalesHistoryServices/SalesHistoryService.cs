using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.DTOs.SalesHistory;

namespace POS_SYSTEM_MVC.Services.SalesHistoryServices
{
    public class SalesHistoryService : ISalesHistoryService
    {
        private readonly POSContext _context;

        public SalesHistoryService(POSContext context)
        {
            _context = context;
        }

        public async Task<SalesHistoryDto> GetSalesHistoryAsync(string? filter = "all")
        {
            var query = _context.Sales
                .Include(s => s.Cashier)
                .Include(s => s.SaleLines)
                .AsQueryable();

            if (filter == "today")
                query = query.Where(s => s.CreatedAt.Date == DateTime.Today);

            var sales = await query.OrderByDescending(s => s.CreatedAt).ToListAsync();

            var saleDtos = sales.Select(s => new SaleDto
            {
                Id = s.Id,
                CashierName = $"{s.Cashier?.FirstName} {s.Cashier?.LastName}".Trim(),
                ItemsCount = s.SaleLines.Sum(l => l.Quantity),
                Subtotal = s.SaleLines.Sum(l => l.OriginalUnitPrice * l.Quantity),
                DiscountAmount = s.DiscountAmount,
                Total = s.SaleLines.Sum(l => l.OriginalUnitPrice * l.Quantity) - (s.DiscountAmount ?? 0),
                Status = s.Status.ToString(),
            }).ToList();

            return new SalesHistoryDto
            {
                TotalRevenue = saleDtos.Sum(s => s.Total),
                TotalOrders = saleDtos.Count,
                AverageOrder = saleDtos.Count > 0 ? saleDtos.Average(s => s.Total) : 0,
                Sales = saleDtos
            };
        }
    }
}