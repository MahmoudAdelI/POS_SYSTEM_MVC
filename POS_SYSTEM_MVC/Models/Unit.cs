using System.ComponentModel.DataAnnotations;

namespace POS_SYSTEM_MVC.Models
{
    public class Unit
    {
        public int Id { get; set; }

        [MaxLength(25)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; } = [];
    }
}
