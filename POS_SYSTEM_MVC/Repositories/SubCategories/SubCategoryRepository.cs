using Microsoft.EntityFrameworkCore;
using POS_SYSTEM_MVC.Data;
using POS_SYSTEM_MVC.DTOs.Attribute;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.Repositories.Base;

namespace POS_SYSTEM_MVC.Repositories.SubCategories;

public class SubCategoryRepository(POSContext context)
    : BaseRepository<SubCategory>(context), ISubCategoryRepository
{
    public async Task<IEnumerable<AttributeWithValuesDto>> GetAttributesWithValuesAsync(int subCategoryId)
    {
        return await _context.ProductAttributeValues
            .Where(v => v.SubCategoryId == subCategoryId)
            .GroupBy(v => new { v.Attribute.Id, v.Attribute.Name })
            .Select(g => new AttributeWithValuesDto
            {
                Id = g.Key.Id,
                Name = g.Key.Name,
                Values = g.Select(v => new AttributeValueDto
                {
                    Id = v.Id,
                    Value = v.Value
                }).ToList()
            }).ToListAsync();

        //return await _context.SubCategoryAttributes
        //.Where(sca => sca.SubCategoryId == subCategoryId)
        //.Select(sca => new AttributeWithValuesDto
        //{
        //    Id = sca.Attribute.Id,
        //    Name = sca.Attribute.Name,
        //    Values = sca.Attribute.Values
        //        .Where(v => v.SubCategoryId == subCategoryId)
        //        .Select(v => new AttributeValueDto { Id = v.Id, Value = v.Value })
        //        .ToList()
        //})
        //.ToListAsync();
    }
}