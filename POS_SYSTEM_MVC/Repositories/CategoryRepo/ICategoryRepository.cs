using POS_SYSTEM_MVC.DTOs.Category;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.CategoryRepo;

public interface ICategoryRepository : IBaseRepository<Category>
{
    Task<IReadOnlyList<CategoryWithSubsDto>> GetAllWithSubsAsync();
}
