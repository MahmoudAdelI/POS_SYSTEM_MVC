using POS_SYSTEM_MVC.Models;
namespace POS_SYSTEM_MVC.Services.UnitServices

{
    public interface IUnitService
    {
        Task<IEnumerable<Unit>> GetAllAsync();
        Task<Unit?> GetByIdAsync(int id);
        Task AddAsync(Unit unit);
        Task UpdateAsync(Unit unit);
        Task DeleteAsync(int id);
    }
}
