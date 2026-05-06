using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs
{
    public class AddCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}