using POS_SYSTEM_MVC.DTOs;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.CategoryServices
{
    public class CategoryService(IUnitOfWork unitOfWork) : ICategoryService
    {
        public async Task<int> AddCategoryAsync(AddCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            await unitOfWork.Categories.AddAsync(category);
            await unitOfWork.SaveChangesAsync();
            return category.Id;
        }

        public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync()
        {
            return await unitOfWork.Categories.GetAllAsync(c => c.SubCategories);
        }
    }
}