using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs.Attribute
{
    public class AddAttributeDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
