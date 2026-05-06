using POS_SYSTEM_MVC.DTOs;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.CategoryRepo;
using POS_SYSTEM_MVC.Repositories.SubCategories;

using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.SubCategoriesServices
{
    public class SubCategoryService(
        ISubCategoryRepository subCategoryRepository,
        ICategoryRepository categoryRepository,
        IUnitOfWork unitOfWork) : ISubCategoryService
    {
        public async Task<int> AddSubCategoryAsync(AddSubCategoryDto dto)
        {
            var category = await categoryRepository.GetAsync(c => c.Name == dto.CategoryName);

            if (category is null)
                throw new Exception($"Category '{dto.CategoryName}' Not Found");

            var subCategory = new SubCategory
            {
                Name = dto.SubCategoryName,
                CategoryId = category.Id
            };

            await subCategoryRepository.AddAsync(subCategory);
            await unitOfWork.SaveChangesAsync();

            return subCategory.Id;
        }
    }
}