using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace POS_SYSTEM_MVC.Repositories.DiscountRepo
{
    public interface IDiscountRepository : IBaseRepository<Discount>
    {
        Task<(IEnumerable<Discount> Discounts, int TotalCount)> GetPaginatedAsync(string searchTerm, int page, int pageSize, POS_SYSTEM_MVC.Constants.Enums.DiscountTypeENUM? filterType = null, bool? filterIsActive = null);
    }
}
