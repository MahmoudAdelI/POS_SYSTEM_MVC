using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.UnitRepo
{
    public class UnitRepository(POSContext context)
       : BaseRepository<Unit>(context), IUnitRepository
    {
        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }
    }
}
