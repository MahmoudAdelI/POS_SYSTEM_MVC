using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static POS_SYSTEM_MVC.Constants.Enums;

namespace POS_SYSTEM_MVC.Models
{
    public class SaleLine
    {
        public int SaleId { get; set; }
        public int ProductVariantId { get; set; }

        public int Quantity { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal OriginalUnitPrice { get; set; }

        public DiscountTypeENUM? DiscountType { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DiscountValue { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? DiscountAmount { get; set; }

        public Sale Sale { get; set; }
        public ProductVariant ProductVariant { get; set; }
    }
}
