using POS_SYSTEM_MVC.DTOs.Attribute;
using POS_SYSTEM_MVC.DTOs.SubCategory;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.SubCategoriesServices
{
    public class SubCategoryService(IUnitOfWork unitOfWork) : ISubCategoryService
    {
        public async Task<SubCategoryResponseDto> AddSubCategoryAsync(AddSubCategoryDto dto)
        {
            var category = await unitOfWork.Categories.GetByIdAsync(dto.CategoryId);
                                           
            if (category is null)
                throw new Exception($"Category '{dto.CategoryId}' Not Found");

            var subCategory = new SubCategory
            {
                Name = dto.Name,
                CategoryId = category.Id
            };

            await unitOfWork.SubCategories.AddAsync(subCategory);
            await unitOfWork.SaveChangesAsync();
            return new SubCategoryResponseDto { Id = subCategory.Id, Name = subCategory.Name };
        }

        public async Task<IEnumerable<AttributeWithValuesDto>> GetAttributesWithValuesAsync(int subCategoryId)
        {
            return await unitOfWork.SubCategories.GetAttributesWithValuesAsync(subCategoryId);
        }
    }
}