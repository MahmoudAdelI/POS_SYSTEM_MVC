using POS_SYSTEM_MVC.DTOs.Category;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.CategoryServices
{
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        public async Task<CategoryResponseDto> AddCategoryAsync(AddCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            await unitOfWork.Categories.AddAsync(category);
            await unitOfWork.SaveChangesAsync();
            return new CategoryResponseDto { Id = category.Id, Name = category.Name };
        }

        public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync()
        {
            return await unitOfWork.Categories.GetAllAsync(c => c.SubCategories);
        }

        public Task<IReadOnlyList<CategoryWithSubsDto>> GetAllWithSubsAsync()
        {
            return unitOfWork.Categories.GetAllWithSubsAsync();
        }
    }
}