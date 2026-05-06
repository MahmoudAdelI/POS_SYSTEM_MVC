
using POS_SYSTEM_MVC.DTOs;
using POS_SYSTEM_MVC.Models;

namespace POS_SYSTEM_MVC.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<int> AddCategoryAsync(AddCategoryDto dto);
        Task<IReadOnlyList<Category>> GetAllCategoriesAsync();
    }
}