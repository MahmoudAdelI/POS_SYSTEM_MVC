using POS_SYSTEM_MVC.DTOs;
using POS_SYSTEM_MVC.Repositories.CategoryRepo;
using POS_SYSTEM_MVC.Services.CategoryServices;
using POS_SYSTEM_MVC.UnitOfWork;
using POS_SYSTEM_MVC.Models;


namespace POS_SYSTEM_MVC.Services.CategoryServices
{
    public class CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork)
        : ICategoryService
    {
        public async Task<int> AddCategoryAsync(AddCategoryDto dto)
        {
            var category = new Category { Name = dto.Name };
            await categoryRepository.AddAsync(category);
            await unitOfWork.SaveChangesAsync();
            return category.Id;
        }

        public async Task<IReadOnlyList<Category>> GetAllCategoriesAsync()
        {
            return await categoryRepository.GetAllAsync(c => c.SubCategories);
        }
    }
}