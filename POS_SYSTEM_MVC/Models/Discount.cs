using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SYSTEM_MVC.Models
{
    public enum DiscountType { Fixed, Percentage }

    public class Discount
    {
        public int Id { get; set; }

        public DiscountType Type { get; set; }

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
            DiscountType.Fixed => "Fixed",
            DiscountType.Percentage => "Percentage",
            _ => "Unknown"
        };

    }
}
