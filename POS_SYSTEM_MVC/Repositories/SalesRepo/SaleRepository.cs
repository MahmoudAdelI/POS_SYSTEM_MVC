using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.SalesRepo
{
    public class SaleRepository(POSContext context) : BaseRepository<Sale>(context), ISaleRepository
    {
        public async Task<IReadOnlyList<Sale>> GetSalesWithDetailsAsync()
        {
            return await _context.Sales
                .Include(s => s.SaleLines)
                    .ThenInclude(sl => sl.ProductVariant)
                        .ThenInclude(pv => pv.Product)
                .ToListAsync();
        }
    }
}
