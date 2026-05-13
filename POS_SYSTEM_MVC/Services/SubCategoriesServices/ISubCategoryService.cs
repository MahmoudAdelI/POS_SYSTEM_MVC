using POS_SYSTEM_MVC.DTOs.SubCategory;

namespace POS_SYSTEM_MVC.Services.SubCategoriesServices;

public interface ISubCategoryService
{
    Task<SubCategoryResponseDto> AddSubCategoryAsync(AddSubCategoryDto dto);
}