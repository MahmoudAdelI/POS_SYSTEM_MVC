using System.ComponentModel.DataAnnotations.Schema;
using static POS_SYSTEM_MVC.Constants.Enums;

namespace POS_SYSTEM_MVC.Models
{

    public class Discount
    {
        public int Id { get; set; }

        public string? Name { get; set; }

        public DiscountTypeENUM Type { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal Value { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }


        [Column(TypeName = "smallmoney")]
        public decimal? SaleTotalThreshold { get; set; }
        public int? ProductId { get; set; }
        public int? ProductVariantId { get; set; }

        public Product? Product { get; set; }
        public ProductVariant? ProductVariant { get; set; }


        [NotMapped]
        public string TypeName => Type switch
        {
            DiscountTypeENUM.Fixed => "Fixed",
            DiscountTypeENUM.Percentage => "Percentage",
            _ => "Unknown"
        };

    }
}
