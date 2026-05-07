using POS_SYSTEM_MVC.DTOs.SubCategory;
using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs.Category
{
    public class AddCategoryDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}