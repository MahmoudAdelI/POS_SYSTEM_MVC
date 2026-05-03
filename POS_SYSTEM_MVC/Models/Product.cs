using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace POS_SYSTEM_MVC.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [MaxLength(50)]
        public string Name { get; set; }

        [Column(TypeName = "smallmoney")]
        public decimal BasePrice { get; set; }


        public int UnitId { get; set; }
        public int BrandId { get; set; }
        public int SubCategoryId { get; set; }

        public Unit Unit { get; set; }
        public Brand Brand { get; set; }
        public SubCategory SubCategory { get; set; }

        public ICollection<ProductVariant> Variants { get; set; } = [];
        public ICollection<Discount> Discounts { get; set; } = [];

    }
}
