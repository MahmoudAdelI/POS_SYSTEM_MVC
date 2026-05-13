using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs.Attribute
{
    public class AddAttributeValueDto
    {
        [Required, MaxLength(50)]
        public string? Value { get; set; }
        [Required]
        public int? AttributeId { get; set; }
        [Required]
        public int? SubcategoryId { get; set; }
    }
}
