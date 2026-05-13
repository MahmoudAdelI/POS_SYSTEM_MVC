using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.Models
{
    public class ProductAttribute
    {
        public int Id { get; set; }

        [ MaxLength(25)]
        public string Name { get; set; } = null!;

        public ICollection<ProductAttributeValue> Values { get; set; } = [];
    }
}
