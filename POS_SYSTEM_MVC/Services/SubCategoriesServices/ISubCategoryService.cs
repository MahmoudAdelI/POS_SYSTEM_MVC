using POS_SYSTEM_MVC.DTOs;

namespace POS_SYSTEM_MVC.Services.SubCategoriesServices;

public interface ISubCategoryService
{
    Task<int> AddSubCategoryAsync(AddSubCategoryDto dto);
}