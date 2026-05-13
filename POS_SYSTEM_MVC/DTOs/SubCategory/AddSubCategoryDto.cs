// DTOs/AddSubCategoryDto.cs
using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs.SubCategory
{

    public class AddSubCategoryDto
    {
        [Required]
        [Range(0, int.MaxValue)]
        public int CategoryId { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}