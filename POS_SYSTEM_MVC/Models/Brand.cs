using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.Models
{
    public class Brand
    {
        public int Id { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = [];
    }
}
