
using POS_SYSTEM_MVC.DTOs;

namespace POS_SYSTEM_MVC.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<int> AddCategoryAsync(AddCategoryDto dto);
    }
}