using POS_SYSTEM_MVC.Models;

namespace POS_SYSTEM_MVC.DTOs.Attribute
{
    public class AttributeWithValuesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<AttributeValueDto> Values { get; set; }
    }
}
