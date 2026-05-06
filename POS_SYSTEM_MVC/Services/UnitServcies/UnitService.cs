using POS_SYSTEM_MVC.Services.UnitServices;
using POS_SYSTEM_MVC.UnitOfWork;
using POS_SYSTEM_MVC.Models;

namespace POS_SYSTEM_MVC.Services.Unitservices
{
    public class UnitService(IUnitOfWork unitOfWork) : IUnitService
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<IEnumerable<Unit>> GetAllAsync()
        {
            return await _unitOfWork.Units.GetAllAsync();
        }

        public async Task<Unit?> GetByIdAsync(int id)
        {
            return await _unitOfWork.Units.GetByIdAsync(id);
        }

        public async Task AddAsync(Unit unit)
        {
            await _unitOfWork.Units.AddAsync(unit);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task UpdateAsync(Unit unit)
        {
            _unitOfWork.Units.Update(unit);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            _unitOfWork.Units.Delete(x => x.Id == id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
