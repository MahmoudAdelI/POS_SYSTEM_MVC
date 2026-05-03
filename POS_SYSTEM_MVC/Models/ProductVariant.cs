using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SYSTEM_MVC.Models
{
    public class ProductVariant
    {
        public int Id { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal UnitPrice { get; set; }

        public int StockQuantity { get; set; }

        [MaxLength(100)]
        public string SKU { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public ICollection<Discount> Discounts { get; set; } = [];
        public ICollection<VariantAttribute> VariantAttributes { get; set; } = [];
        public ICollection<SaleLine> SaleLines { get; set; } = [];
    }
}
