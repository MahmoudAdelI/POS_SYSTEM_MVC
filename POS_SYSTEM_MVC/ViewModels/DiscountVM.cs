using POS_SYSTEM_MVC.Models;
using System.ComponentModel.DataAnnotations;
using static POS_SYSTEM_MVC.Constants.Enums;
namespace POS_SYSTEM_MVC.ViewModels
{
    public class DiscountVM
    {
        [Required(ErrorMessage = "Discount name is required")]
        public string Name { get; set; }

        [Required]
        public DiscountTypeENUM Type { get; set; }
        [Required]

        public decimal DiscountValue { get; set; }
        [Required]

        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime? EndDate { get; set; }
        public decimal? SaleTotalThreshold { get; set; }

        public int? ProductId { get; set; }
        public int? ProductVariantId { get; set; }
        [Required]

        public bool IsActive { get; set; } = true;

    }
}
