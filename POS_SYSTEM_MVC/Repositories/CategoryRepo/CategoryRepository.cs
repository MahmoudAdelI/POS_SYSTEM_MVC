using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.DTOs.Category;
using POS_SYSTEM_MVC.DTOs.SubCategory;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.CategoryRepo
{
    public class CategoryRepository(POSContext context)
        : BaseRepository<Category>(context), ICategoryRepository
    {
        public async Task<IReadOnlyList<CategoryWithSubsDto>> GetAllWithSubsAsync()
        {
            return await _dbset.Select(c => new CategoryWithSubsDto
            {
                Id = c.Id,
                Name = c.Name,

                SubCategories = c.SubCategories.Select(sc => new SubCategoryResponseDto
                {
                    Id = sc.Id,
                    Name = sc.Name,
                }).ToList()


            }).ToListAsync();
        }
    }
}
