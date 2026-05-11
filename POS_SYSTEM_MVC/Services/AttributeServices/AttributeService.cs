
using POS_SYSTEM_MVC.DTOs.Attribute;
using POS_SYSTEM_MVC.Models;
using POS_SYSTEM_MVC.UnitOfWork;

namespace POS_SYSTEM_MVC.Services.AttributeServices
{
    public class AttributeService(IUnitOfWork _uow) : IAttributeService
    {
        public async Task<AttributeResponseDto> AddAttributeAsync(string name)
        {
            name = name.Trim().ToLower();
            var exists = await _uow.Attributes.GetAsync(a => a.Name == name);
            if (exists != null) return new AttributeResponseDto(exists);

            var newAttribute = new ProductAttribute{ Name = name };
            await _uow.Attributes.AddAsync(newAttribute);
            await _uow.SaveChangesAsync();
            return new AttributeResponseDto(newAttribute);
        }

        public async Task<AttributeValueDto> AddAttributeValueAsync(AddAttributeValueDto dto)
        {
            var newAttributeValue = new ProductAttributeValue {
                Value = dto.Value,
                AttributeId = dto.AttributeId.Value,
                SubCategoryId = dto.SubcategoryId.Value
            };
            await _uow.AttributeValues.AddAsync(newAttributeValue);
            await _uow.SaveChangesAsync();
            return new AttributeValueDto {Id = newAttributeValue.Id, Value = newAttributeValue.Value };
        }
    }
}
