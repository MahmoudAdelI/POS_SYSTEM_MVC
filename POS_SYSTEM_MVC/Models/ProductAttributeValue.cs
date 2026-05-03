using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.Models
{
    public class ProductAttributeValue
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Value { get; set; } = null!;

        public int AttributeId { get; set; }
        public int SubCategoryId { get; set; }

        public ProductAttribute Attribute { get; set; } = null!;
        public SubCategory SubCategory { get; set; } = null!;

        public ICollection<VariantAttribute> VariantAttributes { get; set; } = [];
    }
}
