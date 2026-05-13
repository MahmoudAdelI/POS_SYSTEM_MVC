using POS_SYSTEM_MVC.Models;

namespace POS_SYSTEM_MVC.DTOs.Attribute
{
    public class AttributeResponseDto(ProductAttribute productAttribute)
    {
        public int Id { get; set; } = productAttribute.Id;
        public string Name { get; set; } = productAttribute.Name;
    }
}
