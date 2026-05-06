using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories
{
    public interface IUnitRepository : IBaseRepository<Unit>
    {
        Task<IEnumerable<Unit>> GetAllAsync();
    }
}
