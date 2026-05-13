using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static POS_SYSTEM_MVC.Constants.Enums;


namespace POS_SYSTEM_MVC.Models
{
    public enum SaleStatus { Completed, Canceled }
    public class Sale
    {
        public int Id { get; set; }

        public string CashierId { get; set; }

        public DateTime CreatedAt { get; set; }

        public SaleStatus Status { get; set; }

        public DiscountTypeENUM? DiscountType { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal? DiscountValue { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal? DiscountAmount { get; set; }

        public ApplicationUser Cashier { get; set; }
        public ICollection<SaleLine> SaleLines { get; set; } = [];
    }
}
