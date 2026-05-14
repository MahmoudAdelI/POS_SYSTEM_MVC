using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POS_SYSTEM_MVC.Repositories.DiscountRepo
{
    public class DiscountRepository : BaseRepository<Discount>, IDiscountRepository
    {
        public DiscountRepository(POSContext context) : base(context)
        {

        }

        public async Task<(IEnumerable<Discount> Discounts, int TotalCount)> GetPaginatedAsync(string searchTerm, int page, int pageSize, POS_SYSTEM_MVC.Constants.Enums.DiscountTypeENUM? filterType = null, bool? filterIsActive = null)
        {
            var query = _context.Discounts.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d => d.Name.Contains(searchTerm));
            }

            if (filterType.HasValue)
            {
                query = query.Where(d => d.Type == filterType.Value);
            }

            if (filterIsActive.HasValue)
            {
                query = query.Where(d => d.IsActive == filterIsActive.Value);
            }

            var totalCount = await query.CountAsync();

            var discounts = await query
                .OrderByDescending(d => d.CreatedAt)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (discounts, totalCount);
        }
    }
}
