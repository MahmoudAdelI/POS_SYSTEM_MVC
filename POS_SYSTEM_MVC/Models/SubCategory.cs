using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.Models
{
    public class SubCategory
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }


        public int CategoryId { get; set; }
        public Category Category { get; set; }

        public ICollection<Product> Products { get; set; } = [];
        public ICollection<ProductAttributeValue> AttributeValues { get; set; } = [];

    }
}
