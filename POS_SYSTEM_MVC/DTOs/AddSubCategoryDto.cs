// DTOs/AddSubCategoryDto.cs
using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs;

public class AddSubCategoryDto
{
    [Required]
    [MaxLength(50)]
    public string CategoryName { get; set; } = null!;

    [Required]
    [MaxLength(50)]
    public string SubCategoryName { get; set; } = null!;
}