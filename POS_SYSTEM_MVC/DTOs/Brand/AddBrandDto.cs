using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.DTOs.Brand
{
    public class AddBrandDto
    {
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = null!;
    }
}
